using System;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Interfaces
{
    public interface IField
    {
        double Height { get; }
        double Width { get;  }
    }
}
