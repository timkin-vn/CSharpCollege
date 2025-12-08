using Игра.Wpf.Infrastructure;
using System.Windows;

namespace Игра.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectSetup.Init();
        }
    }
}