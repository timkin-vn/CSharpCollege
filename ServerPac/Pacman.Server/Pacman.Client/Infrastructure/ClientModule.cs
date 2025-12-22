using Ninject.Modules;
using Pacman.Common.Services;
using Pacman.BusinessProxy.Services; 

namespace Pacman.Client.Infrastructure
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}