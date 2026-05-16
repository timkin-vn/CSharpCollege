using FifteenGame.Business.Models;
using FifteenGame.Data.Entities;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            OpenLoginWindow();
        }

        private void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            var loginVm = new LoginViewModel();

            loginVm.OnLoginSuccess += (user) =>
            {
                OpenModeSelectionWindow(user);
                loginWindow.Close();
            };

            loginWindow.DataContext = loginVm;
            loginWindow.Show();
        }

        private void OpenModeSelectionWindow(User user)
        {
            var selectionWindow = new ModeSelectionWindow();
            var selectionVm = new ModeSelectionViewModel(user);

            selectionVm.OnGameModeSelected += (mode) =>
            {
                OpenGameWindow(user, mode);
                selectionWindow.Close();
            };

            selectionWindow.DataContext = selectionVm;
            selectionWindow.Show();
        }

        private void OpenGameWindow(User user, GameMode mode)
        {
            var gameWindow = new MainWindow();
            var gameVm = new MainWindowViewModel(user, mode);

            gameVm.RequestExit += () =>
            {
                OpenModeSelectionWindow(user);
                gameWindow.Close();
            };

            gameWindow.DataContext = gameVm;
            gameWindow.Show();
        }
    }
}