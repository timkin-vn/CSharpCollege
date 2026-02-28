using System;
using System.Windows;
using FifteenGame.Data.Repositories; 

namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        
        private readonly IUserRepository _userRepository = new UserRepository();

        public LoginWindow()
        {
            InitializeComponent();
            txtUsername.Focus();
            txtUsername.SelectAll();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtUsername.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    name = "Капитан";

                string saveJsonToLoad = null;

                
                var user = _userRepository.GetOrCreate(name);

                
                if (!string.IsNullOrEmpty(user.SavedGameJson))
                {
                    var result = MessageBox.Show(
                        $"С возвращением, {user.Username}!\nНайдена сохраненная игра. Хотите продолжить с того же места?",
                        "Сохранение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        saveJsonToLoad = user.SavedGameJson;
                    }
                    else
                    {
                        
                        _userRepository.ClearSavedGame(name);
                    }
                }

                
                var mainWindow = new MainWindow(name);

                if (!string.IsNullOrEmpty(saveJsonToLoad))
                {
                    mainWindow.LoadGame(saveJsonToLoad);
                }

                mainWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}