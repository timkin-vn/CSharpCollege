using System;
using System.Windows;

namespace MemoryGame
{
    public partial class LoginWindow : Window
    {
        private readonly DatabaseService _databaseService;
        public string LoggedInUsername { get; private set; }  // Property to store the logged-in username

        public LoginWindow(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            LoggedInUsername = null; // Initialize to null
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            string authenticatedUsername = _databaseService.AuthenticateUser(username, password);

            if (authenticatedUsername != null)
            {
                LoggedInUsername = authenticatedUsername;
                MessageBox.Show("Login successful!");
                this.DialogResult = true; // Close the window and return true
            }
            else
            {
                ErrorMessageTextBlock.Text = "Invalid username or password.";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessageTextBlock.Text = "Username and password are required.";
                return;
            }

            if (_databaseService.RegisterUser(username, password))
            {
                MessageBox.Show("Registration successful!");
                UsernameTextBox.Clear();
                PasswordBox.Clear();
                ErrorMessageTextBlock.Text = "";
            }
            else
            {
                ErrorMessageTextBlock.Text = "Registration failed. Username may already exist.";
            }
        }

    }
}
