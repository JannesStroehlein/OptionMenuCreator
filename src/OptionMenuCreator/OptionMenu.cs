using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OptionMenuCreator.Attributes;
using OptionMenuCreator.Window;

namespace OptionMenuCreator
{
    public abstract class OptionMenu
    {
        [NotEditable]
        public string Title;

        [NotEditable]
        private System.Windows.Window window;
        [NotEditable]
        private OptionMenuControll menu;
        [NotEditable]
        private Thread windowThread;

        public OptionMenu()
        {
            this.Title = "Options";
            this.windowThread = new Thread(this.ThreadMethod);
            this.windowThread.SetApartmentState(ApartmentState.STA);
            this.windowThread.Name = this.Title;
        }
        public void ShowWindow()
        {
            this.windowThread.Start();
        }
        private Label CreateLabelWithText(String Text)
        {
            Label ret = new Label();
            ret.Content = Text;
            return ret;
        }
        private void ThreadMethod()
        {
            this.window = new System.Windows.Window();
            this.window.Height = 450;
            this.window.Width = 300;
            this.menu = new OptionMenuControll();
            this.BakeMenu();
            this.window.Content = this.menu;
            this.window.Show();
            Dispatcher.Run();
        }
        private void BakeMenu()
        {
            Type caller = this.GetType();
            foreach (PropertyInfo info in caller.GetProperties())
            {
                if (info.CanWrite & info.CanWrite)
                {
                    foreach (Attribute attrib in info.GetCustomAttributes<Attribute>())
                    {
                        if (attrib.GetType() == typeof(NotEditableAttribute))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                        else
                            Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(info.Name);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(info.Name);
                }            
            }
            Console.ResetColor();
        }
    }
}
