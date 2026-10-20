using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.Contracts;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IGameService>().To<GameServiceProxy>();

            NinjectKernel.Instance = kernel;

            base.OnStartup(e);
        }
    }
}