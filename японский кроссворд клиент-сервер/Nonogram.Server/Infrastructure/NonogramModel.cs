using Ninject.Modules;
using Nonogram.Business.Services;
using Nonogram.Common.Repositories;
using Nonogram.Common.Services;
using Nonogram.DataAccess.Repositories;

namespace Nonogram.Server.Infrastructure
{
    public class NonogramModule : NinjectModule
    {
        public override void Load()
        {
            // Репозитории
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepository>().InSingletonScope();

            // Сервисы
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}