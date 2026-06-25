using CheckersGame.Business.Contracts;
using CheckersGame.Business.Services;
using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Wpf.ViewModels;
using Ninject;
using Ninject.Modules;
using FifteenGame.DataAccess.EF.Repositories;

namespace FifteenGame.Wpf.Infrastructure
{
    internal class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();
            Bind<IUserService>().To<UserService>().InSingletonScope();

            Bind<ICheckersGameRepository>().To<CheckersGameRepositoryEF>().InSingletonScope();
            Bind<ICheckersGameService>().To<CheckersGameService>().InSingletonScope();

            Bind<UserLoginWindowViewModel>().ToSelf().InTransientScope();
        }
    }
}