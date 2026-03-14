using FifteenGame.Wpf.ViewModels;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        
        public UserLoginWindowViewModel ViewModel => (UserLoginWindowViewModel)DataContext;

        public LoginWindow()
        {
            InitializeComponent();

            
            DataContext = new UserLoginWindowViewModel();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.TryLogin())
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}