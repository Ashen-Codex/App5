using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App5
{
    public sealed partial class Page11 : Page
    {
        private Habit _currentHabit;

        public Page11()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Habit habit)
            {
                _currentHabit = habit;
                LoadProgress();
            }
        }

        private void LoadProgress()
        {
            if (_currentHabit == null) return;

            HabitNameTextBlock.Text = _currentHabit.Name;
            HabitDescriptionTextBlock.Text = _currentHabit.Description;

            // Прогресс-бар
            ProgressBarFill.Width = 200 * (_currentHabit.Progress / 100.0);
            ProgressTextBlock.Text = $"{_currentHabit.Progress}%";

            // Календарь
            CalendarStackPanel.Children.Clear();
            for (int day = 1; day <= 30; day++)
            {
                var dayCard = new Border
                {
                    Width = 30,
                    Height = 30,
                    CornerRadius = new CornerRadius(15),
                    Background = day <= _currentHabit.Progress * 0.3 ?
                        new SolidColorBrush(Color.FromArgb(255, 0, 120, 212)) :
                        new SolidColorBrush(Colors.LightGray),
                    Margin = new Thickness(2),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var dayText = new TextBlock
                {
                    Text = day.ToString(),
                    FontSize = 12,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                dayCard.Child = dayText;
                CalendarStackPanel.Children.Add(dayCard);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page9));
        }
    }
}
