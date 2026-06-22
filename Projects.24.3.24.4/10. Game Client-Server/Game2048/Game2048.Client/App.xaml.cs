using System.Windows;
using Game2048.Common;

namespace Game2048.Client
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
