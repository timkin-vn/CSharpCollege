using FifteenGame.Business.Services;
using FifteenGame.DataAccess.Repositories;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var userRepository = new UserRepository();
                var gameRepository = new GameRepository();
                var userService = new UserService(userRepository);
                var gameService = new GameService(gameRepository);

                var mainWindow = new MainWindow();
                var mainViewModel = new MainWindowViewModel(gameService, userService);
                mainWindow.DataContext = mainViewModel;

                var loginWindow = new UserLoginWindow();
                var loginViewModel = new UserLoginWindowViewModel(userService);
                loginWindow.DataContext = loginViewModel;
                loginWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                var result = loginWindow.ShowDialog();

                if (result == true && loginViewModel.CurrentUser != null)
                {
                    mainViewModel.SetUser(loginViewModel.CurrentUser);

                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                }
                else
                {
                    Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}