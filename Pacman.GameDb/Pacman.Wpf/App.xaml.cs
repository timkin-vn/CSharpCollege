using System.Windows;
using Ninject;
using Pacman.Business.Services;
using Pacman.Common;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Interfaces.Services;
using Pacman.DataAccess.EF.Repositories;

namespace Pacman.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var kernel = new StandardKernel();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IMapRepository>().To<MapRepository>();
            kernel.Bind<IGameRepository>().To<GameRepository>();
            kernel.Bind<ILeaderboardRepository>().To<LeaderboardRepository>();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<ILeaderboardService>().To<LeaderboardService>();

            NinjectKernel.Initialize(kernel);

            var loginWindow = new LoginWindow();
            MainWindow = loginWindow;
            loginWindow.Show();
        }
    }
}