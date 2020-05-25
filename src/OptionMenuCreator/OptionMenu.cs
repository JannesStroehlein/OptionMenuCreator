using System;
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
            this.windowThread = new Thread(ThreadMethod);
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
            this.window.Content = this.menu;
            this.menu.Left.Children.Add(this.CreateLabelWithText("Left"));
            this.menu.Right.Children.Add(this.CreateLabelWithText("Right"));
            this.window.Show();
            Dispatcher.Run();
        }
    }
}
