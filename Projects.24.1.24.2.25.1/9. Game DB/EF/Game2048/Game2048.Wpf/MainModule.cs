using Game2048.Business;
using Game2048.Common.Interfaces;
using Game2048.DataAccess.EF;
using Ninject.Modules;

namespace Game2048.Wpf
{

    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameRepository>().To<GameRepositoryEf>();
            Bind<IUserRepository>().To<UserRepositoryEf>();
            Bind<IGameService>().To<GameService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
