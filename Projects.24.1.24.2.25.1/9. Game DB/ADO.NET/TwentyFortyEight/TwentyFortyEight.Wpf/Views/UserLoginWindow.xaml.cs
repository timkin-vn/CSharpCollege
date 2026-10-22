using System.Windows;
using System.Windows.Input;
using TwentyFortyEight.Wpf.ViewModels;

namespace TwentyFortyEight.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel =>
            (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            CommitLogin();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CommitLogin();
        }

        private void CommitLogin()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.UserName))
            {
                MessageBox.Show("Введите имя игрока.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Always create or find the user
            ViewModel.CreateUser();
            ViewModel.CommitUser();
            DialogResult = true;
        }
    }
}
