using Ninject.Modules;
using Pacman.Common.Services;
using Pacman.Business.Services;
using Pacman.Data.Repositories;
using Ninject.Web.Common; 

namespace Pacman.Server.Infrastructure
{
    public class PacmanModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<UserRepository>().ToSelf().InRequestScope();

            
            Bind<IUserService>().To<UserService>().InRequestScope();
            Bind<IGameService>().To<GameService>().InSingletonScope(); 
        }
    }
}