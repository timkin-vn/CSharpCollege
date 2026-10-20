using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.DataAccess.EF.Repositories;
using Ninject.Modules;

namespace FifteenGame.WebApi.Infrastructure
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IMazeGameRepository>().To<MazeGameRepositoryEF>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}