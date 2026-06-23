using SeaBattle.Business;
using SeaBattle.Common.Interfaces;
using SeaBattle.DataAccess.EF;
using Ninject.Modules;

namespace SeaBattle.WebApi
{
    public class ServerModule : NinjectModule
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
