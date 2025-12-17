using System;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page12 : Page
    {
        public Page12()
        {
            InitializeComponent();
        }

        private void RunTest_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (!int.TryParse(NumberTextBox.Text, out int n) || n <= 0)
            {
                ShowError("Введите положительное число");
                return;
            }

            RunProceduralTest(n);
            RunOopTest(n);
            RunFunctionalTest(n);
        }

        private void ClearResults()
        {
            ProceduralResultTextBlock.Text = "0";
            ProceduralTimeTextBlock.Text = "0 мс";

            OopResultTextBlock.Text = "0";
            OopTimeTextBlock.Text = "0 мс";

            FunctionalResultTextBlock.Text = "0";
            FunctionalTimeTextBlock.Text = "0 мс";
        }

        private void ShowError(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Ошибка: {message}");
        }

        private void RunProceduralTest(int n)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int result = ProceduralCalculator.SumOfSquares(n);
            stopwatch.Stop();

            ProceduralResultTextBlock.Text = result.ToString();
            ProceduralTimeTextBlock.Text = $"{stopwatch.ElapsedMilliseconds} мс";
        }

        private void RunOopTest(int n)
        {
            var calculator = new OopCalculator();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int result = calculator.Calculate(n);
            stopwatch.Stop();

            OopResultTextBlock.Text = result.ToString();
            OopTimeTextBlock.Text = $"{stopwatch.ElapsedMilliseconds} мс";
        }

        private void RunFunctionalTest(int n)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int result = FunctionalCalculator.SumOfSquares(n);
            stopwatch.Stop();

            FunctionalResultTextBlock.Text = result.ToString();
            FunctionalTimeTextBlock.Text = $"{stopwatch.ElapsedMilliseconds} мс";
        }
    }
}
