using SeaBattle.Business;
using SeaBattle.Common.Interfaces;
using SeaBattle.DataAccess;
using Ninject.Modules;

namespace SeaBattle.Wpf
{

    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameRepository>().To<GameRepositoryAdo>();
            Bind<IUserRepository>().To<UserRepositoryAdo>();
            Bind<IGameService>().To<GameService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
