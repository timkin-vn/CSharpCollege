using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;
using Checkers.Common.Contracts;
using Checkers.Common.Infrastructure;
using Checkers.Common.Repositories;
using Checkers.ViewModels;
using Checkers.Views;
using Ninject;
using Ninject.Modules;

namespace Checkers
{
    public class CheckersModule : NinjectModule
    {
        private readonly string _connectionString;
        public CheckersModule(string connectionString)
        {   
            _connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IGameRepository>().To<GameRepository>().WithConstructorArgument("connectionString", _connectionString);
            Bind<Game>().ToSelf().InTransientScope();
            Bind<GameViewModel>().ToSelf().WithConstructorArgument("game", context => context.Kernel.Get<Game>());
            Bind<IGameRepository>().To<GameRepository>();
            Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("connectionString", _connectionString);
            Bind<IViewModelFactory>().To<ViewModelFactory>();
            Bind<AuthService>().ToSelf().InTransientScope();
            Bind<MainWindow>().ToSelf();
            Bind<UserLoginWindow>().ToSelf();
        }
    }
}
