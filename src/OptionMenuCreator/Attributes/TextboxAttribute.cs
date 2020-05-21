using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionMenuCreator.Attributes
{
    public class TextboxAttribute : Attribute
    {
        public bool multiLine { get; private set; }
        public int maxChars { get; private set; }
        public TextboxAttribute(bool multiLine, int maxChars)
        {
            this.multiLine = multiLine;
            this.maxChars = maxChars;
        }
    }
}
