using Nonogram.Common.Infrastructure;
using Nonogram.Wpf.Infrastructure;
using Ninject;
using System;
using System.Windows;

namespace Nonogram.Wpf
{
    public partial class App : Application
    {
        private IKernel _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Инициализируем Ninject
                _kernel = new StandardKernel(new NonogramModule());
                NinjectKernel.Instance = _kernel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}