using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionMenuCreator;
using OptionMenuCreator.Attributes;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
    class Menu : OptionMenu
    {
        [Slider(0, 1)]
        public double d1 = 0.24;

        [Textbox(false, 16)]
        public string t1 = "Hallo Welt!";

        public Menu() : base()
        {
            this.title = "TestMenu";
        }
    }
}
