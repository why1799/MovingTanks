using System;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Interfaces
{
    public interface IFieldObject
    {
        double Height { get; }
        double Width { get; }
        double X { get; }
        double Y { get; }
        void Move();
    }
}
