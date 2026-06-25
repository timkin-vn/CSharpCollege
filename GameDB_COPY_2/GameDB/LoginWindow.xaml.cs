using GameDB.Common.Helpers;
using GameDB.DataAccess;
using System.Windows;

namespace GameDB.Wpf
{
    public partial class LoginWindow : Window
    {
        private readonly UnitOfWork _unitOfWork;
        public int PlayerId { get; private set; }
        public string Username { get; private set; } = string.Empty;

        public LoginWindow(UnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            UsernameBox.Focus();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageText.Text = "Заполните все поля!";
                return;
            }

            var passwordHash = PasswordHelper.HashPassword(password);
            var isValid = await _unitOfWork.Players.AuthenticateAsync(username, passwordHash);

            if (isValid)
            {
                var player = await _unitOfWork.Players.GetByUsernameAsync(username);
                PlayerId = player!.PlayerId;
                Username = username;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageText.Text = "Неверный логин или пароль!";
                PasswordBox.Clear();
                PasswordBox.Focus();
            }
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageText.Text = "Заполните все поля!";
                return;
            }

            if (username.Length < 3)
            {
                MessageText.Text = "Логин должен быть не менее 3 символов!";
                return;
            }

            if (password.Length < 4)
            {
                MessageText.Text = "Пароль должен быть не менее 4 символов!";
                return;
            }

            var exists = await _unitOfWork.Players.UsernameExistsAsync(username);
            if (exists)
            {
                MessageText.Text = "Пользователь уже существует!";
                return;
            }

            var passwordHash = PasswordHelper.HashPassword(password);
            PlayerId = await _unitOfWork.Players.CreateAsync(username, passwordHash);
            Username = username;

            MessageBox.Show($"Пользователь {username} успешно зарегистрирован!", 
                "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }
    }
}