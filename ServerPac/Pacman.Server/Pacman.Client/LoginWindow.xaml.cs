using Pacman.Client.ViewModels;
using System.Windows;

namespace Pacman.Client
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;
            
            vm.TryLogin(this);
        }
    }
}