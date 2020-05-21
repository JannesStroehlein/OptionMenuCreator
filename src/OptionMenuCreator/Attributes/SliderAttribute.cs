using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionMenuCreator.Attributes
{
    public class SliderAttribute : Attribute
    {
        public double minValue { get; private set; }
        public double maxValue { get; private set; }

        public SliderAttribute(double minValue, double maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}
