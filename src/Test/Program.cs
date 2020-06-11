using System;
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
        [Slider(0, 10)]
        public int i1 { get; set; }

        [Textbox(false, 16)]
        public string t1 { get; set; }
        [Textbox(true, 140)]
        public string t2 { get; set; }
        public int i2 { get; set; }
        public bool b1 { get; set; }

        public Menu() : base()
        {
            this.Title = "TestMenu";
        }
    }
}
