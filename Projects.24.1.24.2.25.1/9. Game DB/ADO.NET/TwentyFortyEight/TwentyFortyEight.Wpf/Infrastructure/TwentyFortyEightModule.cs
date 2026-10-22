using Ninject.Modules;
using TwentyFortyEight.Business.Services;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.DataAccess.Repositories;

namespace TwentyFortyEight.Wpf.Infrastructure
{
    internal class TwentyFortyEightModule : NinjectModule
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
