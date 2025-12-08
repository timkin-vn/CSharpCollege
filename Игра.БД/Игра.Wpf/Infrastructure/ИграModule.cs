using Игра.Business.Services;
using Игра.Common.Repositories;
using Игра.Common.Services;
using Игра.DataAccess.EF.Repositories;
using Ninject.Modules;

namespace Игра.Wpf.Infrastructure
{
    internal class ИграModule : NinjectModule
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