using FifteenGame.BusinessProxy.Services; 
using FifteenGame.Common.Services;        
using Ninject.Modules;

namespace FifteenGame.Wpf.Infrastructure 
{
    
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}