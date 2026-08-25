using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.DataAccess.EF.Repositories;
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
            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}
