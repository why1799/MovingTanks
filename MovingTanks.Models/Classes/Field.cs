using MovingTanks.Models.Interfaces;
using Newtonsoft.Json;

namespace MovingTanks.Models.Classes
{
    public class Field : IField
    {
        public Field() { }

        [JsonConstructor]
        public Field(double Width, double Height)
        {
            this.Height = Height;
            this.Width = Width;
        }
        public double Height { get;}
        public double Width { get; }
    }
}
