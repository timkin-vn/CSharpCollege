using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Infrastructure;
using FifteenGame.Wpf.Views;
using Ninject;
using System.Windows;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NinjectKernel.Instance = new StandardKernel(new FifteenGameModule());

            // Сначала окно входа, после входа открывается окно игры в шашки
            var loginWindow = new UserLoginWindow();
            loginWindow.Show();
        }
    }
}
