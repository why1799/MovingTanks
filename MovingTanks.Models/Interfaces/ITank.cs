using System;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Interfaces
{
    public interface ITank : IFieldObject
    {
        double Direction { get; set; }
    }
}
