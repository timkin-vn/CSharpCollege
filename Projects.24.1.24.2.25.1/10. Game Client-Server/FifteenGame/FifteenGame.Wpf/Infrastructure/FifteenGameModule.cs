using CheckersGame.Business.Contracts;
using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Wpf.ViewModels;
using Ninject.Modules;

namespace FifteenGame.Wpf.Infrastructure
{
    internal class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            // Клиент работает с сервером через прокси-сервисы
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<ICheckersGameService>().To<CheckersGameServiceProxy>().InSingletonScope();

            Bind<UserLoginWindowViewModel>().ToSelf().InTransientScope();
        }
    }
}
