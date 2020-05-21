using System;
using OptionMenuCreator.Attributes;

namespace OptionMenuCreator
{
    public abstract class OptionMenu
    {
        [NotEditable]
        public string title;

        public OptionMenu()
        {

        }
    }
}
