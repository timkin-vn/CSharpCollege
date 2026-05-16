using Pacman.Business.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System;
using System.Linq;
using PacmanGame.DataAccess.UnitOfWork;
using PacmanGame.DataAccess;

namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        private readonly GameDataService _gameDataService;

        public LoginWindow()
        {
            InitializeComponent();
            var context = new PacmanDbContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            _gameDataService = new GameDataService(unitOfWork);
        }

        // Этот метод вызывается кнопкой "Войти"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== LOGIN BUTTON CLICKED ===");

            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            // Проверяем, что поля не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Debug.WriteLine("❌ Empty fields");
                ShowMessage("Введите логин и пароль", true);
                return;
            }

            Debug.WriteLine($"Calling Authenticate: {username}");

            // 1. Теперь получаем ID пользователя (int). 0 означает неудачу.
            int userId = _gameDataService.Authenticate(username, password);

            Debug.WriteLine($"Authenticate result: User ID = {userId}");

            // 2. Проверяем, что ID больше 0
            if (userId > 0)
            {
                Debug.WriteLine("✅ Authentication successful");
                // 3. Открываем главное окно, ПЕРЕДАВАЯ ID пользователя
                var mainWindow = new MainWindow(userId);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                Debug.WriteLine("❌ Authentication failed");
                ShowMessage("Неверный логин или пароль", true);
            }
        }

        
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== REGISTER BUTTON CLICKED ===");

            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            Debug.WriteLine($"Username: {username}, Password length: {password.Length}");

            // Проверяем, что поля не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Debug.WriteLine("❌ Empty fields");
                ShowMessage("Введите логин и пароль", true);
                return;
            }

            // Проверяем минимальную длину
            if (username.Length < 3)
            {
                Debug.WriteLine("❌ Username too short");
                ShowMessage("Логин должен содержать минимум 3 символа", true);
                return;
            }

            if (password.Length < 3)
            {
                Debug.WriteLine("❌ Password too short");
                ShowMessage("Пароль должен содержать минимум 3 символа", true);
                return;
            }

            Debug.WriteLine("Calling Register method...");

            // Регистрируем пользователя
            bool success = _gameDataService.Register(username, password);

            Debug.WriteLine($"Register result: {success}");

            if (success)
            {
                Debug.WriteLine("✅ Registration successful");
                ShowMessage("Регистрация успешна! Теперь вы можете войти.", false);
                // Очищаем поля
                UsernameTextBox.Clear();
                PasswordBox.Clear();
            }
            else
            {
                Debug.WriteLine("❌ Registration failed");
                ShowMessage("Пользователь с таким логином уже существует", true);
            }
        }

        private void ShowMessage(string message, bool isError = true)
        {
            MessageText.Text = message;
            if (isError)
            {
                MessageText.Foreground = Brushes.Red;
            }
            else
            {
                MessageText.Foreground = Brushes.Green;
            }
            MessageText.Visibility = Visibility.Visible;
        }
    }
}