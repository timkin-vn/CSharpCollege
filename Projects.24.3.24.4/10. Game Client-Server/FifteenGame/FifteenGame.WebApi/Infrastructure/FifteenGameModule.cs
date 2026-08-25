using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.WebApi.Infrastructure
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();

            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();
        }
    }
}