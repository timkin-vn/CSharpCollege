using Blackjack.Business.Services;
using Blackjack.Common.Contracts.Services;
using Blackjack.DataAccess.DataContext;
using Blackjack.DataAccess.Repositories;
using Blackjack.Wpf.Infrastructure;
using Blackjack.Wpf.ViewModels;
using Blackjack.Wpf.Views;
using System;
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

            IUserService userService;
            IGameService gameService;
            try
            {
                DatabaseInitializer.Initialize(AppSettings.ConnectionString);
                userService = new UserService(new UserRepository(AppSettings.ConnectionString));
                gameService = new GameService(new GameRepository(AppSettings.ConnectionString));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось подключиться к базе данных PostgreSQL.\n" +
                    "Проверьте, что сервер запущен и пароль в AppSettings.cs верный.\n\n" + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

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
