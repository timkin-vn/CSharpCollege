using MemoryGame.BusinessProxy.Services;
using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Contracts.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Wpf.Infrastructure
{
    internal class MemoryGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}
