using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.DataAccess.Repositories;
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
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}