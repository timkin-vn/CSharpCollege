using LightsOutGame.BusinessProxy.Services;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
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
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}
