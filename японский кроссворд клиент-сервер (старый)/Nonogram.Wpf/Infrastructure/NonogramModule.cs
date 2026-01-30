using Ninject.Modules;
using Nonogram.BusinessProxy.Services;
using Nonogram.Common.Services;
using Nonogram.Wpf.ViewModels;
using System;
using System.Net.Http;

namespace Nonogram.Wpf.Infrastructure
{
    internal class NonogramModule : NinjectModule
    {
        public override void Load()
        {
            Console.WriteLine("Loading Ninject module with proxy services...");

            try
            {
                // Создаем HttpClient для прокси
                Bind<HttpClient>().ToConstant(new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:port/") // Заменить на реальный URL
                });

                // Сервисы через прокси
                Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
                Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();

                // ViewModels
                Bind<MainWindowViewModel>().ToSelf().InSingletonScope();

                Console.WriteLine("Ninject module loaded successfully with proxy services");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Ninject module: {ex.Message}");
                throw;
            }
        }
    }
}