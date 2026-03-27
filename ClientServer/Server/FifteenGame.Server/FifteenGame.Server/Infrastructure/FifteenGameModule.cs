using Ninject.Modules;


using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;


using FifteenGame.Business.Services;


using FifteenGame.Server.DataAccess.EF.Repositories;

namespace FifteenGame.Server.Infrastructure
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();

            
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}