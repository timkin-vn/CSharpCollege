using CheckersGame.Business.Contracts;
using CheckersGame.Business.Services;
using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.DataAccess.Repositories;
using FifteenGame.Wpf.ViewModels;
using Ninject;
using Ninject.Modules;

namespace FifteenGame.Wpf.Infrastructure
{
    internal class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepository>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            // Bind<IGameService>().To<GameService>().InSingletonScope();

            Bind<ICheckersGameRepository>().To<CheckersGameRepository>().InSingletonScope();
            Bind<ICheckersGameService>().To<CheckersGameService>().InSingletonScope();

            // Регистрируем UserLoginWindowViewModel как self, чтобы можно было внедрить зависимости
            Bind<UserLoginWindowViewModel>().ToSelf().InTransientScope();
        }
    }
}