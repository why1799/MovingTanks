using MovingTanks.Models.Interfaces;
using System;

namespace MovingTanks.Models.Classes
{
    public enum Directions{
        Up = 0, Right, Down, Left
    }
    public static class State
    {
        public static IField Field { get; set; }
        public static IFieldObjects Objects { get; set; }
        public static sbyte Speed { get; set; }
        public static bool IsOk { get; private set; }
        public static void SetCheckPosition()
        {
            IsOk = CheckPosition();
        }
        public static bool CheckPosition()
        {
            bool ok = true;
            for (var i = 0; i < Objects.Count && ok; i++)
            {
                ok = !IsObjectOutFromTheField(Objects[i]);
                for (var j = i + 1; j < Objects.Count && ok; j++)
                {
                    ok = !AreObjectsInTouch(Objects[i], Objects[j]);
                }
            }
            return ok;
        }

        private static bool CheckPointAndObject(double x1, double y1, double x2, double y2, double width, double height)
        {
            return !(x1 >= x2 && x1 <= x2 + width && y1 >= y2 && y1 <= y2 + height);
        }

        public static bool AreObjectsInTouch(IFieldObject fieldobject1, IFieldObject fieldobject2)
        {
            return !(CheckPointAndObject(fieldobject1.X, fieldobject1.Y, fieldobject2.X, fieldobject2.Y, fieldobject2.Width, fieldobject2.Height)
                        && CheckPointAndObject(fieldobject1.X + fieldobject1.Width, fieldobject1.Y, fieldobject2.X, fieldobject2.Y, fieldobject2.Width, fieldobject2.Height)
                        && CheckPointAndObject(fieldobject1.X, fieldobject1.Y + fieldobject1.Height, fieldobject2.X, fieldobject2.Y, fieldobject2.Width, fieldobject2.Height)
                        && CheckPointAndObject(fieldobject1.X + fieldobject1.Width, fieldobject1.Y + fieldobject1.Height, fieldobject2.X, fieldobject2.Y, fieldobject2.Width, fieldobject2.Height));
        }

        public static bool IsObjectOutFromTheField(IFieldObject fieldobject)
        {
            return !(!CheckPointAndObject(fieldobject.X, fieldobject.Y, 0, 0, Field.Width, Field.Height)
                        && !CheckPointAndObject(fieldobject.X + fieldobject.Width, fieldobject.Y, 0, 0, Field.Width, Field.Height)
                        && !CheckPointAndObject(fieldobject.X, fieldobject.Y + fieldobject.Height, 0, 0, Field.Width, Field.Height)
                        && !CheckPointAndObject(fieldobject.X + fieldobject.Width, fieldobject.Y + fieldobject.Height, 0, 0, Field.Width, Field.Height));
        }

        public static void TankIsOutFromField(ITank tank)
        {
            if(tank.X <= 0)
            {
                LeftBlock(tank);
            }
            else if(tank.Y <= 0)
            {
                UpBlock(tank);
            }
            else if (tank.Y + tank.Height >= Field.Height)
            {
                DownBlock(tank);
            }
            else if (tank.X + tank.Width >= Field.Width)
            {
                RightBlock(tank);
            }
        }

        public static void ObjectsAreInTouch(IFieldObject fieldobject1, IFieldObject fieldobject2)
        {
            if(fieldobject1 is Tank && fieldobject1 is Tank)
            {
                DoDirChange(fieldobject1 as ITank, fieldobject2);
                DoDirChange(fieldobject2 as ITank, fieldobject1);
            }
            else if(fieldobject1 is Tank)
            {
                var tank = fieldobject1 as ITank;
                var obstacle = fieldobject2 as IFieldObject;

                DoDirChange(tank, obstacle);
            }
            else if(fieldobject2 is Tank)
            {
                var obstacle = fieldobject1 as IFieldObject;
                var tank = fieldobject2 as ITank;

                DoDirChange(tank, obstacle);
            }
        }

        private static void DoDirChange(ITank tank, IFieldObject obstacle)
        {
            ITank saveTank = new Tank(tank.Width, tank.Height, tank.X, tank.Y);
            saveTank.Direction = tank.Direction;

            var dir = DirectionsForTank(tank);

            if (dir.Length == 1)
            {
                DoDir(dir[0], tank);
            }

            else
            {
                double left = Math.Max(tank.X, obstacle.X);
                double top = Math.Min(tank.Y + tank.Height, obstacle.Y + obstacle.Height);
                double right = Math.Min(tank.X + tank.Width, obstacle.X + obstacle.Width);
                double bottom = Math.Max(tank.Y, obstacle.Y);

                double width = right - left;
                double height = top - bottom;

                if (width > height)
                {
                    if(dir[0] == Directions.Down || dir[0] == Directions.Up)
                    {
                        DoDir(dir[0], tank);
                    }
                    else if (dir[1] == Directions.Down || dir[1] == Directions.Up)
                    {
                        DoDir(dir[1], tank);
                    }
                }
                else
                {
                    if (dir[0] == Directions.Left || dir[0] == Directions.Right)
                    {
                        DoDir(dir[0], tank);
                    }
                    else if (dir[1] == Directions.Left || dir[1] == Directions.Right)
                    {
                        DoDir(dir[1], tank);
                    }
                }
            }
        }

        private static void DoDir(Directions dir, ITank tank)
        {
            if (dir == Directions.Down)
                DownBlock(tank);
            else if (dir == Directions.Left)
                LeftBlock(tank);
            else if (dir == Directions.Up)
                UpBlock(tank);
            else if (dir == Directions.Right)
                RightBlock(tank);
        }

        private static Directions[] DirectionsForTank(ITank tank)
        {
            if (tank.Direction == 0 || tank.Direction == 360)
            {
                return new Directions[] { Directions.Up };
            }
            else if(tank.Direction == 90)
            {
                return new Directions[] { Directions.Right };
            }
            else if (tank.Direction == 180)
            {
                return new Directions[] { Directions.Down };
            }
            else if (tank.Direction == 270)
            {
                return new Directions[] { Directions.Left };
            }
            else if (tank.Direction > 0 && tank.Direction < 90)
            {
                return new Directions[] { Directions.Up, Directions.Right };
            }
            else if (tank.Direction > 90 && tank.Direction < 180)
            {
                return new Directions[] { Directions.Down, Directions.Right };
            }
            else if (tank.Direction > 180 && tank.Direction < 270)
            {
                return new Directions[] { Directions.Down, Directions.Left };
            }
            else 
            {
                return new Directions[] { Directions.Up, Directions.Left };
            }
        }

        private static void LeftBlock(ITank tank)
        {
            if(tank.Direction == 270)
            {
                tank.Direction = 90;
            }
            else
            {
                tank.Direction = 360 - tank.Direction;
            }
        }

        private static void UpBlock(ITank tank)
        {
            if (tank.Direction == 0 || tank.Direction == 360)
            {
                tank.Direction = 180;
            }
            else if(tank.Direction < 360 && tank.Direction > 270)
            {
                tank.Direction = 540 - tank.Direction;
            }
            else if (tank.Direction < 90)
            {
                tank.Direction = 180 - tank.Direction;
            }
        }

        private static void RightBlock(ITank tank)
        {
            if (tank.Direction == 90)
            {
                tank.Direction = 270;
            }
            else
            {
                tank.Direction = 360 - tank.Direction;
            }
        }

        private static void DownBlock(ITank tank)
        {
            if (tank.Direction == 180)
            {
                tank.Direction = 0;
            }
            else if(tank.Direction < 180)
            {
                tank.Direction = 180 - tank.Direction;
            }
            else if (tank.Direction > 180)
            {
                tank.Direction = 540 - tank.Direction;
            }
        }
    }
}
