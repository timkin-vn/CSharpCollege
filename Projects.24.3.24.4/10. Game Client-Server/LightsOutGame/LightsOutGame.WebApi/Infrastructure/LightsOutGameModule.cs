using LightsOutGame.Business.Services;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightsOutGame.WebApi.Infrastructure
{
    public class LightsOutGameModule : NinjectModule
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