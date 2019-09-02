using MovingTanks.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Classes
{
    public class Obstacle : IFieldObject
    {
        [JsonConstructor]
        public Obstacle(double Width, double Height, double X, double Y)
        {
            this.Height = Height;
            this.Width = Width;
            this.X = X;
            this.Y = Y;
        }
        public double Height { get; }
        public double Width { get;}
        public double X { get;}
        public double Y { get; }

        public void Move()
        {
            return;
        }
    }
}
