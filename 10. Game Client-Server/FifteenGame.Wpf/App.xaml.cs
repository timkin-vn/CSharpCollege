using FifteenGame.Common.Services;
using FifteenGame.BusinessProxy.Services;
using FifteenGame.Wpf.Infrastructure;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using Ninject;
using System;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        public App()
        {

            var kernel = NinjectKernel.Instance;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                OpenLoginWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска: " + ex.ToString());
            }
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

        private void OpenModeSelectionWindow(Common.BusinessDtos.UserDto user)
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

        private void OpenGameWindow(Common.BusinessDtos.UserDto user, Common.Enums.GameMode mode)
        {
            var gameWindow = new MainWindow();
            var gameVm = new MainWindowViewModel(user.Id, mode);

            gameVm.RequestMenu += () =>
            {
                OpenModeSelectionWindow(user);

                gameWindow.Close();
            };

            gameWindow.DataContext = gameVm;
            gameWindow.Show();
        }
    }
}