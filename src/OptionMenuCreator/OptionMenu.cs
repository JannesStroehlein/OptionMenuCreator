using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        [NotEditable]
        private Type[] typeWhiteList = { typeof(string), typeof(int), typeof(double), typeof(float), typeof(bool), typeof(char) };
        [NotEditable]
        private const int OptionHeight = 30;

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
            ret.Height = OptionHeight;
            ret.Content = Text;
            ret.VerticalAlignment = VerticalAlignment.Center;
            return ret;
        }
        private void ThreadMethod()
        {
            this.window = new System.Windows.Window();
            this.window.Height = 350;
            this.window.Width = 400;
            this.window.Title = this.Title;
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
                    bool isEditable = true;
                    foreach (Attribute attrib in info.GetCustomAttributes<Attribute>())
                    {
                        if (attrib.GetType() == typeof(NotEditableAttribute))
                        {
                            isEditable = false;
                            break;
                        }
                        else
                            isEditable = true;                     
                    }
                    if (isEditable)
                    {
                        bool isWhiteListed = false;
                        foreach (Type test in this.typeWhiteList)
                        {
                            if (test == info.PropertyType)
                            {
                                isWhiteListed = true;
                                break;
                            }
                        }
                        if (!isWhiteListed)
                            continue;
                    }
                    else
                        continue;
                    // Option Name
                    this.menu.Left.Children.Add(this.CreateLabelWithText(info.Name));

                    // Option Input
                    if (info.PropertyType == typeof(float) | info.PropertyType == typeof(double) | info.PropertyType == typeof(int)) //Numeric Input
                    {
                        if (info.GetCustomAttribute<SliderAttribute>() != null)
                        {
                            SliderAttribute attribute = info.GetCustomAttribute<SliderAttribute>();
                            Slider slider = new Slider();
                            slider.Height = OptionHeight;
                            slider.ToolTip = 0;
                            if (info.GetValue(this) != null)
                                slider.Value = Convert.ToDouble(info.GetValue(this)); // Set current value
                            slider.VerticalAlignment = VerticalAlignment.Center;
                            slider.ValueChanged += delegate(object sender, RoutedPropertyChangedEventArgs<double> e)
                            {
                                slider.ToolTip = slider.Value;
                                if (info.PropertyType == typeof(int))
                                    info.SetValue(this, Convert.ToInt32(slider.Value));
                                else
                                    info.SetValue(this, slider.Value);
                            };
                            slider.Minimum = attribute.minValue;
                            slider.Maximum = attribute.maxValue;
                            if (info.PropertyType == typeof(int))
                            {
                                slider.TickFrequency = 1;
                                slider.IsSnapToTickEnabled = true;
                            }
                            this.menu.Right.Children.Add(slider);
                        }
                        else
                        {
                            TextBox textBox = new TextBox();
                            textBox.Height = OptionHeight;
                            if (info.GetValue(this) != null)
                                textBox.Text = info.GetValue(this).ToString(); // Set current value
                            textBox.VerticalAlignment = VerticalAlignment.Center;
                            textBox.PreviewTextInput += delegate (object sender, TextCompositionEventArgs e)
                            {
                                Regex _regex = new Regex("[^0-9.,-]+");
                                e.Handled = _regex.IsMatch(e.Text);
                            };
                            textBox.TextChanged += delegate (object sender, TextChangedEventArgs e)
                            {
                                if (info.PropertyType == typeof(double))
                                    info.SetValue(this, Convert.ToDouble(textBox.Text));
                                else if (info.PropertyType == typeof(float))
                                    info.SetValue(this, float.Parse(textBox.Text));
                                else if (info.PropertyType == typeof(int))
                                    info.SetValue(this, Convert.ToInt32(textBox.Text));
                            };
                            this.menu.Right.Children.Add(textBox);
                        }
                    }
                    else if (info.PropertyType == typeof(string) | info.PropertyType == typeof(char)) // Text Input
                    {
                        TextBox textBox = new TextBox();
                        textBox.Height = OptionHeight;
                        if (info.GetValue(this) != null)
                            textBox.Text = info.GetValue(this).ToString(); // Set current value
                        textBox.TextChanged += delegate (object sender, TextChangedEventArgs e)
                        {
                            info.SetValue(this, textBox.Text);
                        };
                        if (info.GetCustomAttribute<TextboxAttribute>() != null)
                        {
                            TextboxAttribute attribute = info.GetCustomAttribute<TextboxAttribute>();
                            textBox.MaxLength = attribute.maxChars;
                            if (attribute.multiLine)
                                textBox.MaxLines = 999;
                        }
                        else
                            textBox.MaxLines = 1;
                        this.menu.Right.Children.Add(textBox);
                    }
                    else if (info.PropertyType == typeof(bool))
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Height = OptionHeight;
                        if (info.GetValue(this) != null)
                            checkBox.IsChecked = (bool)info.GetValue(this); // Set current value
                        checkBox.VerticalAlignment = VerticalAlignment.Center;
                        checkBox.HorizontalAlignment = HorizontalAlignment.Center;
                        checkBox.Checked += delegate (object sender, RoutedEventArgs e)
                        {
                            info.SetValue(this, checkBox.IsChecked);
                        };
                        this.menu.Right.Children.Add(checkBox);
                    }
                    else
                        this.menu.Right.Children.Add(this.CreateLabelWithText(" ")); // Add dummy
                }       
            }
        }
    }
}