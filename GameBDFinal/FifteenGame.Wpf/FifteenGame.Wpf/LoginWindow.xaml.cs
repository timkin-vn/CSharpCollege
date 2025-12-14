using System;
using System.Windows;
using FifteenGame.Data.Repositories; // <-- Подключили

namespace FifteenGame.Wpf
{
    public partial class LoginWindow : Window
    {
        // Создаем экземпляр репозитория
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

                // Используем репозиторий для поиска/создания игрока
                var user = _userRepository.GetOrCreate(name);

                // Проверка на наличие сохранения
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
                        // Отказались — очищаем через репозиторий
                        _userRepository.ClearSavedGame(name);
                    }
                }

                // Запуск главного окна
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