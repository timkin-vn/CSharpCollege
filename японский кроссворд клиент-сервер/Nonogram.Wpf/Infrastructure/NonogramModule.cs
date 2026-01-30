using Ninject.Modules;
using Nonogram.BusinessProxy.Services;
using Nonogram.Common.Services;
using Nonogram.Wpf.ViewModels;
using Nonogram.Wpf.Views;
using System;

namespace Nonogram.Wpf.Infrastructure
{
    internal class NonogramModule : NinjectModule
    {
        public override void Load()
        {
            Console.WriteLine("=== Загрузка модуля Ninject ===");

            try
            {
                // Сервисы (через прокси)
                Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
                Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();

                // ViewModels
                Bind<MainWindowViewModel>().ToSelf().InTransientScope();
                Bind<UserLoginWindowViewModel>().ToSelf().InTransientScope();

                // Views (окна)
                Bind<UserLoginWindow>().ToSelf().InTransientScope();
                Bind<MainWindow>().ToSelf().InTransientScope();

                Console.WriteLine("Модуль Ninject загружен успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки модуля Ninject: {ex.Message}");
                throw;
            }
        }
    }
}