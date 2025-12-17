using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace App5
{
    public sealed partial class Page7 : Page
    {
        private BookService _bookService = new BookService();
        private List<Book> _books = new List<Book>();
        private Book _selectedBook;

        public Page7()
        {
            InitializeComponent();
            Loaded += async (s, e) => await LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            _books = await _bookService.GetAllBooksAsync();
            UpdateBooksUI();
        }

        private void UpdateBooksUI()
        {
            BooksStackPanel.Children.Clear();

            foreach (var book in _books)
            {
                var card = new Border
                {
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10),
                    MinWidth = 300,
                    Height = 60
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                var idText = new TextBlock
                {
                    Text = $"[{book.Id}]",
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 120, 212)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 0, 10, 0),
                    MinWidth = 40
                };

                var titleText = new TextBlock
                {
                    Text = $"{book.Title} - {book.Author} ({book.ISBN})",
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Center
                };

                grid.Children.Add(idText);
                Grid.SetColumn(titleText, 1);
                grid.Children.Add(titleText);

                card.Child = grid;
                card.Tapped += (s, e) => SelectBook(book);
                BooksStackPanel.Children.Add(card);
            }
        }

        private void SelectBook(Book book)
        {
            _selectedBook = book;
            IdTextBox.Text = book.Id.ToString();
            TitleTextBox.Text = book.Title;
            AuthorTextBox.Text = book.Author;
            IsbnTextBox.Text = book.ISBN;
            YearTextBox.Text = book.PublishYear.ToString();
            AvailabilityToggle.IsOn = book.Available;

            ErrorTextBlock.Text = $"Выбрана книга: {book.Title}";
            ErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
        }

        private async void RefreshBooks_Click(object sender, RoutedEventArgs e)
        {
            await LoadBooksAsync();
            ErrorTextBlock.Text = "Список обновлен";
            ErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
        }

        private async void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorTextBox.Text))
            {
                ErrorTextBlock.Text = "Заполните обязательные поля";
                return;
            }

            var book = new Book
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                ISBN = IsbnTextBox.Text,
                PublishYear = int.TryParse(YearTextBox.Text, out int year) ? year : DateTime.Now.Year,
                Available = AvailabilityToggle.IsOn
            };

            bool success = await _bookService.CreateBookAsync(book);

            if (success)
            {
                await LoadBooksAsync();
                ClearForm();
                ErrorTextBlock.Text = "Книга добавлена";
                ErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ErrorTextBlock.Text = "Ошибка при добавлении";
            }
        }

        private async void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook == null)
            {
                ErrorTextBlock.Text = "Сначала выберите книгу";
                return;
            }

            _selectedBook.Title = TitleTextBox.Text;
            _selectedBook.Author = AuthorTextBox.Text;
            _selectedBook.ISBN = IsbnTextBox.Text;
            _selectedBook.PublishYear = int.TryParse(YearTextBox.Text, out int year) ? year : _selectedBook.PublishYear;
            _selectedBook.Available = AvailabilityToggle.IsOn;

            bool success = await _bookService.UpdateBookAsync(_selectedBook.Id, _selectedBook);

            if (success)
            {
                await LoadBooksAsync();
                ErrorTextBlock.Text = "Книга обновлена";
                ErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ErrorTextBlock.Text = "Ошибка при обновлении";
            }
        }

        private async void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook == null)
            {
                ErrorTextBlock.Text = "Сначала выберите книгу";
                return;
            }

            bool success = await _bookService.DeleteBookAsync(_selectedBook.Id);

            if (success)
            {
                await LoadBooksAsync();
                ClearForm();
                ErrorTextBlock.Text = "Книга удалена";
                ErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ErrorTextBlock.Text = "Ошибка при удалении";
            }
        }

        private void ClearForm()
        {
            _selectedBook = null;
            IdTextBox.Text = "";
            TitleTextBox.Text = "";
            AuthorTextBox.Text = "";
            IsbnTextBox.Text = "";
            YearTextBox.Text = "";
            AvailabilityToggle.IsOn = true;
        }
    }
}
