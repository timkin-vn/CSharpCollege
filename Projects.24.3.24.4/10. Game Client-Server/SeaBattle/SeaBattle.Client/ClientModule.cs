using SeaBattle.BusinessProxy;
using SeaBattle.Common.Interfaces;
using Ninject.Modules;

namespace SeaBattle.Client
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
