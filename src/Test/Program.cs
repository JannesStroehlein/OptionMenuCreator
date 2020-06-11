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
            Menu menu = new Menu();
            menu.ShowWindow();
            Console.ReadKey();
        }
    }
    class Menu : OptionMenu
    {
        [Slider(0, 1)]
        public double d1 { get; set; }

        [Textbox(false, 16)]
        public string t1 { get; set; }

        public Menu() : base()
        {
            this.Title = "TestMenu";
        }
    }
}
