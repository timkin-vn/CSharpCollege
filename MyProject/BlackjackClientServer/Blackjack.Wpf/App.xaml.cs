using Blackjack.BusinessProxy.Services;
using Blackjack.Common.Contracts.Services;
using Blackjack.Wpf.ViewModels;
using Blackjack.Wpf.Views;
using System.Windows;

namespace Blackjack.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Пока открыто окно входа, приложение не должно закрываться само
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Вместо настоящих сервисов — прокси, которые ходят на сервер Blackjack.WebApi
            IUserService userService = new UserServiceProxy();
            IGameService gameService = new GameServiceProxy();

            var loginWindow = new UserLoginWindow(userService);
            if (loginWindow.ShowDialog() != true)
            {
                Shutdown();
                return;
            }

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(gameService, loginWindow.User),
            };
            MainWindow = mainWindow;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            mainWindow.Show();
        }
    }
}
