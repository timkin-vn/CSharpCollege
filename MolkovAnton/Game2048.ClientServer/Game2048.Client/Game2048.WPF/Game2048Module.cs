using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.ClientProxy.Services;
using Game2048.Common.Services;
using Ninject.Modules;

namespace Game2048.WPF
{
    internal class Game2048Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserServiceProxy>().InSingletonScope();
            Bind<IGameService>().To<GameServiceProxy>().InSingletonScope();
        }
    }
}
