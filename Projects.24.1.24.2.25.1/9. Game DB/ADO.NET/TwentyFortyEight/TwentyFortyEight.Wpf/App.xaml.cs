using Ninject;
using System.Windows;
using TwentyFortyEight.Common.Infrastructure;
using TwentyFortyEight.Wpf.Infrastructure;

namespace TwentyFortyEight.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NinjectKernel.Instance = new StandardKernel(new TwentyFortyEightModule());
        }
    }
}
