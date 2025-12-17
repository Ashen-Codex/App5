using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App5
{
    public sealed partial class Page9 : Page
    {
        private List<Habit> _habits = new List<Habit>();
        private const int DefaultProgress = 75;

        public Page9()
        {
            InitializeComponent();
            InitializeDefaultHabits();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Обработка переданной привычки
            if (e.Parameter is Habit newHabit)
            {
                _habits.Add(newHabit);
                UpdateHabitsUI();
            }
        }

        private void InitializeDefaultHabits()
        {
            _habits = new List<Habit>
            {
                new Habit { Name = "Пить воду", Description = "2 литра в день", Progress = 75 },
                new Habit { Name = "Прогулка", Description = "30 минут в день", Progress = 45 },
                new Habit { Name = "Чтение", Description = "30 минут в день", Progress = 20 }
            };
            UpdateHabitsUI();
        }

        private void UpdateHabitsUI()
        {
            HabitsStackPanel.Children.Clear();

            foreach (var habit in _habits)
            {
                var card = new Border
                {
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10),
                    Height = 80
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var nameText = new TextBlock
                {
                    Text = habit.Name,
                    FontSize = 18,
                    FontWeight = FontWeights.SemiBold,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 10, 0)
                };

                var descriptionText = new TextBlock
                {
                    Text = habit.Description,
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    VerticalAlignment = VerticalAlignment.Center
                };

                var progressText = new TextBlock
                {
                    Text = $"{habit.Progress}%",
                    FontSize = 18,
                    FontWeight = FontWeights.SemiBold,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(10, 0, 0, 0)
                };

                var stackPanel = new StackPanel { Spacing = 5 };
                stackPanel.Children.Add(nameText);
                stackPanel.Children.Add(descriptionText);

                grid.Children.Add(stackPanel);
                Grid.SetColumn(progressText, 1);
                grid.Children.Add(progressText);

                card.Child = grid;
                card.Tapped += (s, e) => Frame.Navigate(typeof(Page11), habit);
                HabitsStackPanel.Children.Add(card);
            }
        }

        private void CreateHabit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page10));
        }
    }
}
