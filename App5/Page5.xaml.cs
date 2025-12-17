using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page5 : Page
    {
        private Stack<int> _stack = new Stack<int>();
        private Queue<int> _queue = new Queue<int>();

        public Page5()
        {
            InitializeComponent();
        }

        private void AddToStack_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(StackInput.Text, out int value))
            {
                _stack.Push(value);
                UpdateStackUI();
                StackLogTextBlock.Text = $"Добавлено значение {value}";
                StackInput.Text = "";
            }
        }

        private void RemoveFromStack_Click(object sender, RoutedEventArgs e)
        {
            if (_stack.Count > 0)
            {
                int value = _stack.Pop();
                UpdateStackUI();
                StackLogTextBlock.Text = $"Удалено значение {value}";
            }
        }

        private void AddToQueue_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QueueInput.Text, out int value))
            {
                _queue.Enqueue(value);
                UpdateQueueUI();
                QueueLogTextBlock.Text = $"Добавлено значение {value}";
                QueueInput.Text = "";
            }
        }

        private void RemoveFromQueue_Click(object sender, RoutedEventArgs e)
        {
            if (_queue.Count > 0)
            {
                int value = _queue.Dequeue();
                UpdateQueueUI();
                QueueLogTextBlock.Text = $"Удалено значение {value}";
            }
        }

        private void UpdateStackUI()
        {
            StackItemsPanel.Children.Clear();
            foreach (var item in _stack.GetItems())
            {
                CreateItemCard(StackItemsPanel, item);
            }
        }

        private void UpdateQueueUI()
        {
            QueueItemsPanel.Children.Clear();
            foreach (var item in _queue.GetItems())
            {
                CreateItemCard(QueueItemsPanel, item);
            }
        }

        private void CreateItemCard(StackPanel panel, int value)
        {
            var card = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(10),
                MinWidth = 150,
                Height = 40
            };

            var textBlock = new TextBlock
            {
                Text = value.ToString(),
                FontSize = 16,
                VerticalAlignment = VerticalAlignment.Center
            };

            card.Child = textBlock;
            panel.Children.Add(card);
        }
    }
}
