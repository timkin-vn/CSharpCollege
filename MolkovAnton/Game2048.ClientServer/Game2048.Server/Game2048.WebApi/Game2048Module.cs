using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Game2048.Business.Services;
using Game2048.Common.Repositories;
using Game2048.Common.Services;
using Game2048.DataAccess.EF.Repositories;
using Ninject.Modules;

namespace Game2048.WebApi
{
    public class Game2048Module : NinjectModule
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