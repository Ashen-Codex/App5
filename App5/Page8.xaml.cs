using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page8 : Page
    {
        public Page8()
        {
            InitializeComponent();
            AddLog("Приложение запущено. Готово к отладке.");
        }

        private void AddLog(string message)
        {
            LogTextBlock.Text += $"{DateTime.Now:HH:mm:ss} - {message}\n";
        }

        private void NullReference_Click(object sender, RoutedEventArgs e)
        {
            AddLog("Вызван метод с NullReferenceException");
            try
            {
                List<string> items = null;
                for (int i = 0; i < items.Count; i++)
                {
                    Debug.WriteLine($"Item: {items[i]}");
                    AddLog($"Обработка элемента {i}");
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка: {ex.GetType().Name}\n{ex.Message}";
                AddLog($"Ошибка: {ex.GetType().Name} - {ex.Message}");
            }
        }

        private void IndexOutOfBounds_Click(object sender, RoutedEventArgs e)
        {
            AddLog("Вызван метод с IndexOutOfRangeException");
            try
            {
                int[] numbers = { 1, 2, 3 };
                for (int i = 0; i <= numbers.Length; i++)
                {
                    Debug.WriteLine($"Number: {numbers[i]}");
                    AddLog($"Число: {numbers[i]}");
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка: {ex.GetType().Name}\n{ex.Message}";
                AddLog($"Ошибка: {ex.GetType().Name} - {ex.Message}");
            }
        }

        private void DivideByZero_Click(object sender, RoutedEventArgs e)
        {
            AddLog("Вызван метод с DivideByZeroException");
            try
            {
                int a = 10;
                int b = 0;
                int result = a / b;
                Debug.WriteLine($"Result: {result}");
                AddLog($"Результат: {result}");
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка: {ex.GetType().Name}\n{ex.Message}";
                AddLog($"Ошибка: {ex.GetType().Name} - {ex.Message}");
            }
        }
    }
}
