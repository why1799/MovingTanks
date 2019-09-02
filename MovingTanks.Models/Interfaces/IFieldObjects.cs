using System;
using System.Collections.Generic;
using System.Text;

namespace MovingTanks.Models.Interfaces
{
    public interface IFieldObjects : ICollection<IFieldObject>
    {
        IFieldObject this[int index] { get; set; }
        void Move();
    }
}
