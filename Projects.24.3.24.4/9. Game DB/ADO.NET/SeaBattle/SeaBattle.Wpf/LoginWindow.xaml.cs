using System.Windows;
using SeaBattle.Common;
using SeaBattle.Common.Interfaces;

namespace SeaBattle.Wpf
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

            var userService = NinjectKernel.Get<IUserService>();
            var user = userService.Login(name);

            var main = new MainWindow(user);
            main.Show();
            Close();
        }
    }
}
