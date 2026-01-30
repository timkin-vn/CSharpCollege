using Ninject;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Infrastructure;
using Nonogram.Common.Services;
using Nonogram.Wpf.ViewModels;
using System;
using System.Windows;

namespace Nonogram.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        private readonly IUserService _userService;
        private UserModel _userModel;

        public UserLoginWindow()
        {
            try
            {
                Console.WriteLine("=== UserLoginWindow создается ===");

                // Пытаемся получить сервис через NinjectKernel
                if (NinjectKernel.Instance != null)
                {
                    _userService = NinjectKernel.Instance.Get<IUserService>();
                    Console.WriteLine("UserService получен через NinjectKernel");
                }
                else
                {
                    Console.WriteLine("NinjectKernel.Instance is null - возможно, не настроен Ninject");
                    // Создаем временный сервис или бросаем исключение
                    throw new InvalidOperationException("NinjectKernel не инициализирован");
                }

                InitializeComponent();
                Console.WriteLine("UserLoginWindow создан успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания UserLoginWindow: {ex.Message}");
                throw;
            }
        }

        // ЭТОТ МЕТОД ОБЯЗАТЕЛЬНО ДОЛЖЕН БЫТЬ!
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine($"OK button clicked, username: {UserNameTextBox.Text}");

                if (string.IsNullOrWhiteSpace(UserNameTextBox.Text))
                {
                    MessageBox.Show("Введите имя пользователя!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Пробуем найти пользователя
                _userModel = _userService.GetUserByName(UserNameTextBox.Text);

                if (_userModel == null)
                {
                    Console.WriteLine($"User {UserNameTextBox.Text} not found, asking to create");

                    var result = MessageBox.Show($"Пользователь '{UserNameTextBox.Text}' не найден. Создать нового?",
                        "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Console.WriteLine($"Creating new user: {UserNameTextBox.Text}");
                        _userModel = _userService.GetOrCreateUser(UserNameTextBox.Text);
                    }
                    else
                    {
                        return; // Пользователь отказался создавать
                    }
                }
                else
                {
                    Console.WriteLine($"User {UserNameTextBox.Text} found, Id: {_userModel.Id}");
                }

                if (_userModel != null)
                {
                    // Создаем главное окно
                    CreateAndShowMainWindow();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OkButton_Click: {ex.Message}");
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ЭТОТ МЕТОД ОБЯЗАТЕЛЬНО ДОЛЖЕН БЫТЬ!
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Cancel button clicked");
            Application.Current.Shutdown();
        }

        private void CreateAndShowMainWindow()
        {
            try
            {
                // Создаем главную ViewModel
                var mainViewModel = NinjectKernel.Instance?.Get<MainWindowViewModel>();
                if (mainViewModel == null)
                {
                    throw new Exception("Не удалось создать MainWindowViewModel");
                }

                // Устанавливаем пользователя
                mainViewModel.SetUser(_userModel);

                // Создаем главное окно
                var mainWindow = new MainWindow();
                mainWindow.DataContext = mainViewModel;

                // Показываем главное окно
                mainWindow.Show();

                // Закрываем окно входа
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating main window: {ex.Message}");
                MessageBox.Show($"Ошибка создания главного окна: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}