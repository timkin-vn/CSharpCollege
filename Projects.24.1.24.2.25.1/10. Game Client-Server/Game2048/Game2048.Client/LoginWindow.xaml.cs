using System;
using System.Windows;
using Game2048.Common;
using Game2048.Common.Interfaces;

namespace Game2048.Client
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text == null ? string.Empty : NameBox.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Введите имя игрока", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var userService = NinjectKernel.Get<IUserService>();
                var user = userService.Login(name);

                var main = new MainWindow(user);
                main.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
