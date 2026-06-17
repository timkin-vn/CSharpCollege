using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var kernel = new StandardKernel(new FifteenGameModule());
                NinjectKernel.Instance = kernel;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации Ninject: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}