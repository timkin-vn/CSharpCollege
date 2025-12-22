using System;
using System.Windows;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Repositories;

namespace Minesweeper
{
    public partial class UserLogin : Window
    {
        private UserRepository _userRepository;
        private User _currentUser;

        public UserLogin()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Введите имя пользователя!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _currentUser = _userRepository.GetOrCreateUser(username);

                bool continueLastGame = ContinueCheckBox.IsChecked ?? false;

                var mainWindow = new View.MainWindow(_currentUser, continueLastGame);
                mainWindow.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}