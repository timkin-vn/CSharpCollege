using Minesweeper.BusinessProxy;
using Minesweeper.Common.Interfaces;
using Ninject.Modules;

namespace Minesweeper.Client
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameService>().To<GameServiceProxy>();
            Bind<IUserService>().To<UserServiceProxy>();
        }
    }
}
