using Ninject;
using System.Windows;

namespace Game2048.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var settingsModule = new TwentyFortyEightModule();
            IKernel serviceProvider = new StandardKernel(settingsModule);

            Current.Resources["ServiceProvider"] = serviceProvider;
        }
    }
}