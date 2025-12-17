//using FifteenGame.Business.Services;
//using FifteenGame.Common.Repositories;
//using FifteenGame.Common.Services;
//using FifteenGame.DataAccess.Repositories;
//using Ninject.Modules;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FifteenGame.Wpf.Infrastructure
//{
//    internal class FifteenGameModule : NinjectModule
//    {
//        public override void Load()
//        {
//            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
//            Bind<IGameRepository>().To<GameRepository>().InSingletonScope();

//            Bind<IUserService>().To<UserService>().InSingletonScope();
//            Bind<IGameService>().To<GameService>().InSingletonScope();
//        }
//    }
//}
