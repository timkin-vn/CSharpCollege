using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.Infrastructure
{
    internal class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            // Убираем IGameService так как используем прямые HTTP запросы
            // Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}
