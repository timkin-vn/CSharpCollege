using MemoryGame.Business.Services;
using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemoryGame.WebApi.Infrastructure
{
    public class MemoryGameModule : NinjectModule
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