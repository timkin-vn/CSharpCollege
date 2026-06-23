using System.Windows;
using Minesweeper.Common;

namespace Minesweeper.Client
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
