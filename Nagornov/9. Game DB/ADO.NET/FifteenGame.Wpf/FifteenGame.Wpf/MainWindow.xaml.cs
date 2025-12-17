using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("MainWindow инициализирован");

            var userRepository = new FifteenGame.DataAccess.Repositories.UserRepository();
            var gameRepository = new FifteenGame.DataAccess.Repositories.GameRepository();
            var userService = new FifteenGame.Business.Services.UserService(userRepository);
            var gameService = new FifteenGame.Business.Services.GameService(gameRepository);

            this.DataContext = new ViewModels.MainWindowViewModel(gameService, userService);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = LoginUserNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                LoginStatusText.Text = "Введите имя пользователя";
                return;
            }

            try
            {
                var userRepository = new FifteenGame.DataAccess.Repositories.UserRepository();

                if (!userRepository.UserExists(userName))
                {
                    LoginStatusText.Text = "Пользователь не найден";
                    return;
                }

                var userDto = userRepository.GetByName(userName);

                if (userDto == null)
                {
                    LoginStatusText.Text = "Ошибка загрузки";
                    return;
                }

                var viewModel = DataContext as ViewModels.MainWindowViewModel;
                if (viewModel != null)
                {
                    var user = new FifteenGame.Common.BusinessModels.UserModel
                    {
                        Id = userDto.Id,
                        Name = userDto.Name
                    };
                    viewModel.SetUser(user);
                    LoginStatusText.Text = $"Добро пожаловать, {userName}!";
                    LoginUserNameTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                LoginStatusText.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = LoginUserNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                LoginStatusText.Text = "Введите имя";
                return;
            }

            if (userName.Length < 3)
            {
                LoginStatusText.Text = "Минимум 3 символа";
                return;
            }

            try
            {
                var userRepository = new FifteenGame.DataAccess.Repositories.UserRepository();

                if (userRepository.UserExists(userName))
                {
                    LoginStatusText.Text = "Имя уже занято";
                    return;
                }

                var userDto = userRepository.Create(userName);

                var viewModel = DataContext as ViewModels.MainWindowViewModel;
                if (viewModel != null)
                {
                    var user = new FifteenGame.Common.BusinessModels.UserModel
                    {
                        Id = userDto.Id,
                        Name = userDto.Name
                    };
                    viewModel.SetUser(user);
                    LoginStatusText.Text = $"Создан пользователь {userName}!";
                    LoginUserNameTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                LoginStatusText.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModels.MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.ClearUser();
                LoginStatusText.Text = "";
                LoginUserNameTextBox.Focus();
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);
            if (e.OriginalSource is FrameworkElement element &&
                element.DataContext is ViewModels.CellViewModel cell)
            {
                var viewModel = DataContext as ViewModels.MainWindowViewModel;
                viewModel?.ToggleFlag(cell);
                e.Handled = true;
            }
        }

        private void MinesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}