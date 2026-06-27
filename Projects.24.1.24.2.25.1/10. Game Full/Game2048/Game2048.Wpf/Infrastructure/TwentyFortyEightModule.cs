using Ninject.Modules;
using Game2048.BusinessProxy.Services;
using Game2048.Common.Contracts.Services;

namespace Game2048.Wpf
{
    internal class TwentyFortyEightModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}