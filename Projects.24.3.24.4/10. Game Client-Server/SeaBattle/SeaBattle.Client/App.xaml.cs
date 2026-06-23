using System.Windows;
using SeaBattle.Common;
using SeaBattle.Wpf;

namespace SeaBattle.Client
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectKernel.Initialize(new ClientModule());

            var login = new LoginWindow();
            login.Show();
        }
    }
}
