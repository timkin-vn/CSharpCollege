using FifteenGame.Business.Services;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.WebApi
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserEFRepository>().InSingletonScope();
            Bind<IGameRepository>().To<GameEFRepository>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}