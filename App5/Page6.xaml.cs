using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App5
{
    public sealed partial class Page6 : Page
    {
        private UserValidator _validator = new UserValidator();
        private UserDatabase _database = new UserDatabase();

        public Page6()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Сброс ошибок
            RegistrationErrorTextBlock.Text = "";
            RegistrationErrorTextBlock.Foreground = new SolidColorBrush(Colors.Red);

            // Сбор данных
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Валидация
            if (string.IsNullOrWhiteSpace(email))
            {
                RegistrationErrorTextBlock.Text = "Введите email";
                return;
            }

            if (!_validator.ValidateEmail(email))
            {
                RegistrationErrorTextBlock.Text = "Некорректный формат email";
                return;
            }

            if (!_validator.ValidatePassword(password))
            {
                RegistrationErrorTextBlock.Text = "Пароль должен быть не менее 8 символов";
                return;
            }

            if (!_validator.ValidateConfirmPassword(password, confirmPassword))
            {
                RegistrationErrorTextBlock.Text = "Пароли не совпадают";
                return;
            }

            // Хеширование и сохранение
            string passwordHash = _validator.HashPassword(password);
            bool success = _database.AddUser(email, passwordHash);

            if (success)
            {
                RegistrationErrorTextBlock.Text = "Регистрация успешна!";
                RegistrationErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                // Очистка полей
                EmailTextBox.Text = "";
                PasswordBox.Password = "";
                ConfirmPasswordBox.Password = "";
            }
            else
            {
                RegistrationErrorTextBlock.Text = "Пользователь с таким email уже существует";
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Сброс ошибок
            LoginErrorTextBlock.Text = "";
            LoginErrorTextBlock.Foreground = new SolidColorBrush(Colors.Red);

            // Сбор данных
            string email = LoginEmailTextBox.Text;
            string password = LoginPasswordBox.Password;

            // Валидация
            if (string.IsNullOrWhiteSpace(email))
            {
                LoginErrorTextBlock.Text = "Введите email";
                return;
            }

            if (!_validator.ValidateEmail(email))
            {
                LoginErrorTextBlock.Text = "Некорректный формат email";
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                LoginErrorTextBlock.Text = "Введите пароль";
                return;
            }

            // Проверка пользователя
            User user = _database.GetUser(email);
            if (user == null)
            {
                LoginErrorTextBlock.Text = "Пользователь не найден";
                return;
            }

            // Сравнение хешей
            string inputHash = _validator.HashPassword(password);
            if (inputHash == user.PasswordHash)
            {
                LoginErrorTextBlock.Text = "Вход выполнен успешно!";
                LoginErrorTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                // Очистка полей
                LoginEmailTextBox.Text = "";
                LoginPasswordBox.Password = "";
            }
            else
            {
                LoginErrorTextBlock.Text = "Неверный пароль";
            }
        }
    }
}
