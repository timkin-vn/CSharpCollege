using Ninject;
using Nonogram.Common.Infrastructure;
using Nonogram.Wpf.Infrastructure;
using Nonogram.Wpf.Views;
using System;
using System.Windows;

namespace Nonogram.Wpf
{
    public partial class App : Application
    {
        private IKernel _kernel;

        private void Application_Start(object sender, StartupEventArgs e)
        {
            Console.WriteLine("=== Application_Start ===");

            try
            {
                // 1. Сначала конфигурируем Ninject
                Console.WriteLine("Конфигурируем Ninject...");
                ConfigureContainer();

                // 2. Теперь получаем UserLoginWindow через Ninject (у него будут зависимости)
                Console.WriteLine("Создаем UserLoginWindow через Ninject...");
                var loginWindow = _kernel.Get<UserLoginWindow>();
                loginWindow.Show();

                Console.WriteLine("UserLoginWindow показан");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запуске: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Ошибка запуска: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        private void ConfigureContainer()
        {
            try
            {
                Console.WriteLine("=== Конфигурация Ninject контейнера ===");

                // Создаем ядро Ninject с нашим модулем
                _kernel = new StandardKernel(new NonogramModule());

                // Устанавливаем NinjectKernel.Instance для обратной совместимости
                Nonogram.Common.Infrastructure.NinjectKernel.Instance = _kernel;

                Console.WriteLine("Ninject контейнер сконфигурирован успешно");
                Console.WriteLine($"NinjectKernel.Instance установлен: {NinjectKernel.Instance != null}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка конфигурации Ninject: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _kernel?.Dispose();
            base.OnExit(e);
        }
    }
}