using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace App5
{
    public sealed partial class Page4 : Page
    {
        private List<int> _data = new List<int>();
        private const int DefaultSize = 10;
        private const int MaxSize = 100;

        public Page4()
        {
            InitializeComponent();
            Loaded += (s, e) => GenerateArray(DefaultSize);
        }

        private void GenerateArray(int size)
        {
            _data.Clear();
            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                _data.Add(random.Next(1, 1000));
            }

            UpdateArrayUI();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && int.TryParse(textBox.Text, out int value))
            {
                int index = (int)textBox.Tag;
                if (index >= 0 && index < _data.Count)
                {
                    _data[index] = value;
                }
            }
        }

        private void UpdateArrayUI()
        {
            ArrayStackPanel.Children.Clear();

            for (int i = 0; i < _data.Count; i++)
            {
                var card = new Border
                {
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10),
                    MinWidth = 600
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var indexText = new TextBlock
                {
                    Text = $"[{i}]",
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 120, 212)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 10, 0),
                    MinWidth = 40
                };

                var textBox = new TextBox
                {
                    Text = _data[i].ToString(),
                    Height = 44,
                    MinWidth = 250,
                    FontSize = 18,
                    Padding = new Thickness(10, 8, 10, 8),
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)),
                    Background = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0)),
                    CornerRadius = new CornerRadius(6),
                    Tag = i,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.NoWrap
                };

                textBox.LostFocus += (s, e) =>
                {
                    if (s is TextBox tb && int.TryParse(tb.Text, out int value))
                    {
                        int index = (int)tb.Tag;
                        if (index >= 0 && index < _data.Count)
                        {
                            _data[index] = value;
                        }
                    }
                };

                var deleteButton = new Button
                {
                    Content = "×",
                    Width = 40,
                    Height = 40,
                    Background = new SolidColorBrush(Color.FromArgb(30, 255, 0, 0)),
                    Foreground = new SolidColorBrush(Colors.Red),
                    FontSize = 22,
                    CornerRadius = new CornerRadius(20),
                    Tag = i
                };
                deleteButton.Click += (s, e) =>
                {
                    _data.RemoveAt((int)deleteButton.Tag);
                    UpdateArrayUI();
                };

                grid.Children.Add(indexText);
                Grid.SetColumn(textBox, 1);
                grid.Children.Add(textBox);
                Grid.SetColumn(deleteButton, 2);
                grid.Children.Add(deleteButton);

                card.Child = grid;
                ArrayStackPanel.Children.Add(card);
            }
        }

        private void GenerateArray_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ArraySizeTextBox.Text, out int size) && size > 0 && size <= MaxSize)
            {
                GenerateArray(size);
            }
            else
            {
                GenerateArray(DefaultSize);
            }
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            if (_data.Count < MaxSize)
            {
                _data.Add(new Random().Next(1, 1000));
                UpdateArrayUI();
            }
        }

        private void RemoveElement_Click(object sender, RoutedEventArgs e)
        {
            if (_data.Count > 1)
            {
                _data.RemoveAt(_data.Count - 1);
                UpdateArrayUI();
            }
        }

        private void RunTest_Click(object sender, RoutedEventArgs e)
        {
            var array = new int[_data.Count];
            for (int i = 0; i < _data.Count; i++)
            {
                array[i] = _data[i];
            }

            var stopwatch = new Stopwatch();
            string results = SearchAlgorithms.TestAlgorithms(1_000_0);

            int target = array.Length > 0 ? array[array.Length / 2] : 0;
            stopwatch.Start();
            SearchAlgorithms.LinearSearch(array, target);
            stopwatch.Stop();
            results += $"Линейный поиск: {stopwatch.ElapsedMilliseconds} мс (O(n))\n";
            stopwatch.Reset();

            Array.Sort(array);
            stopwatch.Start();
            SearchAlgorithms.BinarySearch(array, target);
            stopwatch.Stop();
            results += $"Бинарный поиск: {stopwatch.ElapsedMilliseconds} мс (O(log n))\n";
            stopwatch.Reset();

            var bubbleArray = (int[])array.Clone();
            stopwatch.Start();
            SearchAlgorithms.BubbleSort(bubbleArray);
            stopwatch.Stop();
            results += $"Сортировка пузырьком: {stopwatch.ElapsedMilliseconds} мс (O(n²))\n";
            stopwatch.Reset();

            var quickArray = (int[])array.Clone();
            stopwatch.Start();
            SearchAlgorithms.QuickSort(quickArray, 0, quickArray.Length - 1);
            stopwatch.Stop();
            results += $"Быстрая сортировка: {stopwatch.ElapsedMilliseconds} мс (O(n log n))";

            ResultsTextBlock.Text = results;
        }
    }
}
