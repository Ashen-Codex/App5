using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace App5
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Page1 : Page
    {


        string current = "0";
        double first = 0;
        string operation = "";
        bool isNew = true;
        Calculator calc = new Calculator();

        private Calculator calculator = new Calculator();

        public Page1()
        {
            InitializeComponent();
        }

        void OnDigit(object sender, RoutedEventArgs e)
        {
            string digit = (sender as Button).Content.ToString();

            if (isNew)
            {
                current = digit;
                isNew = false;
            }
            else
            {
                if (current == "0") current = digit;
                else current += digit;
            }

            Display.Text = current;
        }

        void OnOp(object sender, RoutedEventArgs e)
        {
            operation = (sender as Button).Content.ToString();
            first = double.Parse(current);
            isNew = true;
        }

        void OnEquals(object sender, RoutedEventArgs e)
        {
            if (operation == "") return;

            double second = double.Parse(current);
            try
            {
                double result = calc.Calculate(first, second, operation);
                Display.Text = (result % 1 == 0) ? result.ToString("F0") : result.ToString();
            }
            catch
            {
                Display.Text = "Ошибка";
            }

            operation = "";
            isNew = true;
        }

        void OnClear(object sender, RoutedEventArgs e)
        {
            current = "0";
            operation = "";
            first = 0;
            isNew = true;
            Display.Text = "0";
        }
        
    }
}


