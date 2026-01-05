using Ninject.Modules;
using Nonogram.Business.Services;
using Nonogram.Common.Repositories;
using Nonogram.Common.Services;
using Nonogram.DataAccess.Repositories;
using Nonogram.Wpf.ViewModels;
using System;

namespace Nonogram.Wpf.Infrastructure
{
    internal class NonogramModule : NinjectModule
    {
        public override void Load()
        {
            Console.WriteLine("Loading Ninject module...");

            try
            {
                // Репозитории
                Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
                Bind<IGameRepository>().To<GameRepository>().InSingletonScope();

                // Сервисы
                Bind<IUserService>().To<UserService>().InSingletonScope();
                Bind<IGameService>().To<GameService>().InSingletonScope();

                // ViewModels
                Bind<MainWindowViewModel>().ToSelf().InSingletonScope();

                Console.WriteLine("Ninject module loaded successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Ninject module: {ex.Message}");
                throw;
            }
        }
    }
}