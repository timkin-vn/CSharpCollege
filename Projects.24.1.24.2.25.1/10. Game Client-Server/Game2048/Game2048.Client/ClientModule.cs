using Game2048.BusinessProxy;
using Game2048.Common.Interfaces;
using Ninject.Modules;

namespace Game2048.Client
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
