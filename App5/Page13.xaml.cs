using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page13 : Page
    {
        private StorageFile _selectedFile;

        public Page13()
        {
            InitializeComponent();
        }

        private async void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            _selectedFile = null;

            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");

            _selectedFile = await picker.PickSingleFileAsync();

            if (_selectedFile != null)
            {
                FilePathTextBox.Text = _selectedFile.Path;
                ResultsStackPanel.Children.Clear();
            }
        }

        private async void ProcessFile_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedFile == null)
            {
                ErrorTextBlock.Text = "Сначала выберите файл";
                return;
            }

            try
            {
                var stats = await ProcessFileAsync(_selectedFile);
                DisplayResults(stats);
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка: {ex.Message}";
            }
        }

        private async Task<FileStatistics> ProcessFileAsync(StorageFile file)
        {
            string content = await FileIO.ReadTextAsync(file);

            return new FileStatistics
            {
                LineCount = content.Split('\n').Length,
                WordCount = Regex.Matches(content, @"\b\w+\b").Count,
                CharCount = content.Length,
                LongestWord = FindLongestWord(content),
                MostFrequentLetter = FindMostFrequentLetter(content)
            };
        }

        private string FindLongestWord(string content)
        {
            var words = Regex.Matches(content, @"\b\w+\b")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .OrderByDescending(w => w.Length)
                             .ToList();

            return words.Any() ? words[0] : string.Empty;
        }

        private char FindMostFrequentLetter(string content)
        {
            var letters = content.ToLower()
                                   .Where(char.IsLetter)
                                   .GroupBy(c => c)
                                   .OrderByDescending(g => g.Count())
                                   .ToList();

            return letters.Any() ? letters[0].Key : ' ';
        }

        private void DisplayResults(FileStatistics stats)
        {
            ResultsStackPanel.Children.Clear();

            AddResultCard("Строки", stats.LineCount.ToString());
            AddResultCard("Слова", stats.WordCount.ToString());
            AddResultCard("Символы", stats.CharCount.ToString());
            AddResultCard("Самое длинное слово", stats.LongestWord);
            AddResultCard("Самая частая буква", stats.MostFrequentLetter.ToString());

            SaveResultsAsync(stats);
        }

        private void AddResultCard(string title, string value)
        {
            var card = new Border
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(0, 5, 0, 5),
                Padding = new Thickness(10),
                Height = 50
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var titleText = new TextBlock
            {
                Text = title,
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 10, 0)
            };

            var valueText = new TextBlock
            {
                Text = value,
                FontSize = 16,
                FontWeight = FontWeights.Medium,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            grid.Children.Add(titleText);
            Grid.SetColumn(valueText, 1);
            grid.Children.Add(valueText);

            card.Child = grid;
            ResultsStackPanel.Children.Add(card);
        }

        private async void SaveResultsAsync(FileStatistics stats)
        {
            try
            {
                var savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Текстовые файлы", new List<string> { ".txt" });
                savePicker.SuggestedFileName = "file_stats.txt";

                var file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    var content = $"Строки: {stats.LineCount}\n" +
                                  $"Слова: {stats.WordCount}\n" +
                                  $"Символы: {stats.CharCount}\n" +
                                  $"Самое длинное слово: {stats.LongestWord}\n" +
                                  $"Самая частая буква: {stats.MostFrequentLetter}";

                    await FileIO.WriteTextAsync(file, content);
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Ошибка при сохранении: {ex.Message}";
            }
        }
    }

    public class FileStatistics
    {
        public int LineCount { get; set; }
        public int WordCount { get; set; }
        public int CharCount { get; set; }
        public string LongestWord { get; set; }
        public char MostFrequentLetter { get; set; }
    }
}
