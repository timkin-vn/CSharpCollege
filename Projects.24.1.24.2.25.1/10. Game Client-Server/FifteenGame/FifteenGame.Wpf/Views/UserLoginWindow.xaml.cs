using CheckersGame.Business.Contracts;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.ViewModels;
using Ninject;
using System;
using System.Windows;
using GameModel = CheckersGame.Business.Models.GameModel;

namespace FifteenGame.Wpf.Views
{
    /// <summary>
    /// Interaction logic for UserLoginWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        private readonly UserLoginWindowViewModel _viewModel;

        public UserLoginWindow()
        {
            InitializeComponent();

            _viewModel = NinjectKernel.Instance.Get<UserLoginWindowViewModel>();
            DataContext = _viewModel;
            _viewModel.UserLoggedIn += OnUserLoggedIn;
        }

        private void OnUserLoggedIn(GameModel model)
        {
            var gameService = NinjectKernel.Instance.Get<ICheckersGameService>();

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(model, false, gameService)
            };
            mainWindow.Show();
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_viewModel.FindUser())
                {
                    if (MessageBox.Show("Такого пользователя нет в системе. Создать?", "Вход",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    _viewModel.CreateUser();
                }

                _viewModel.CommitUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
