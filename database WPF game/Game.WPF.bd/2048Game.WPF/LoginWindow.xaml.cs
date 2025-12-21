using System.Windows;
using _2048Game.Business.Services;
using _2048Game.Business.Models;

namespace _2048Game.WPF
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SokobanBoard board = null;
            if (UserService.UserExists(username))
            {
                var path = UserService.GetFilePathForUser(username);
                MessageBox.Show($"Loading saved board from: {path}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);
                board = UserService.LoadBoard(username);
            }

            var main = new MainWindow(username, board);
            main.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // start without username
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}