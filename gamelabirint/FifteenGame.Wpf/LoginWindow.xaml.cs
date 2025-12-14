using System;
using System.Windows;
using FifteenGame.Business.Services;
using FifteenGame.Data.Entities;

namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        private AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();

            try
            {
                _authService = new AuthService();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к сервису:\n{ex.Message}\n\nПроверьте, установлены ли пакеты Npgsql и EntityFramework в проекте WPF.", "Ошибка запуска");
            }
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtLogin.Text;


            string pass = PwdPassword.Password;

            if (_authService == null)
            {
                MessageBox.Show("Сервис авторизации не работает. Проверьте базу данных.");
                return;
            }

            try
            {
                var user = _authService.Login(login, pass);

                if (user != null)
                {
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка входа");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных:\n{ex.Message}\n\nПодробности: {ex.InnerException?.Message}", "Критическая ошибка");
            }
        }


        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtLogin.Text;


            string pass = PwdPassword.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Введите логин и пароль!", "Внимание");
                return;
            }

            if (_authService == null) return;

            try
            {
                var newUser = _authService.Register(login, pass);

                if (newUser != null)
                {
                    MessageBox.Show("Регистрация успешна! Теперь нажмите кнопку ВОЙТИ.", "Успех");
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации:\n{ex.Message}\n\nПодробности: {ex.InnerException?.Message}\n\nПроверьте пароль в App.config!", "Ошибка БД");
            }
        }
    }
}