using System;
using System.Windows;
using Ninject;
using Pacman.Common;
using Pacman.Common.Interfaces.Services;

namespace Pacman.Wpf
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;

        public LoginWindow()
        {
            InitializeComponent();

            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var user = _userService.GetOrCreateUser(username);

                var mainWindow = new MainWindow(user.Id);
                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка: {ex.Message}\n\nСтек вызовов:\n{ex.StackTrace}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
