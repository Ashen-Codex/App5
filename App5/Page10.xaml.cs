using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App5
{
    public sealed partial class Page10 : Page
    {
        public Page10()
        {
            InitializeComponent();
        }

        private void SaveHabit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                ErrorTextBlock.Text = "Введите название привычки";
                return;
            }

            var newHabit = new Habit
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                Target = TargetTextBox.Text,
                Progress = 0
            };

            Frame.Navigate(typeof(Page9), newHabit);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page9));
        }
    }
}
