using System;

namespace OptionMenuCreator.Attributes
{
    public class NumberBoxAttribute : Attribute
    {
        public double minNumber { get; private set; }
        public double maxNumber { get; private set; }
        public NumberBoxAttribute(double minNumber, double maxNumber)
        {
            this.minNumber = minNumber;
            this.maxNumber = maxNumber;
        }
    }
}
