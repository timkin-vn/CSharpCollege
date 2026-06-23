using Minesweeper.Business;
using Minesweeper.Common.Interfaces;
using Minesweeper.DataAccess.EF;
using Ninject.Modules;

namespace Minesweeper.WebApi
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
