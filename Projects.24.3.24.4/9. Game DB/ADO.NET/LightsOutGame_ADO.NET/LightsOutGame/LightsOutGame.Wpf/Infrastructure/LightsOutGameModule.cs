using LightsOutGame.Business.Services;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.DataAccess.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Wpf.Infrastructure
{
    internal class LightsOutGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepository>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IGameService>().To<GameService>().InSingletonScope();
        }
    }
}
