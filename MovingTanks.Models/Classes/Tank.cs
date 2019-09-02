using MovingTanks.Models.Interfaces;
using Newtonsoft.Json;
using System;

namespace MovingTanks.Models.Classes
{
    public class Tank : ITank
    {

        [JsonConstructor]
        public Tank(double Width, double Height, double X, double Y)
        {
            this.Height = Height;
            this.Width = Width;
            this.X = X;
            this.Y = Y;
            Random rnd = new Random();
            Direction = rnd.NextDouble() * 360;
            //Direction = 189;
        }

        [JsonIgnore]
        public double Direction { get; set; }

        public double Height { get; private set; }

        public double Width { get; private set; }

        public double X { get; private set; }

        public double Y { get; private set; }

        public void Move()
        {
            var alpha = Direction * Math.PI / 180;
            var x = X + (0.1 * Math.Sin(alpha));
            var y = Y - (0.1 * Math.Cos(alpha));
            X = x;
            Y = y;
        }
    }
}
