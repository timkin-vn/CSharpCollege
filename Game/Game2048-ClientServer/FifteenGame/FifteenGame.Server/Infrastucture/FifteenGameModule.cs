using FifteenGame.Business;
using FifteenGame.Business.Services;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.EF.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Server.Infrastucture
{
    public class FifteenGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>().InSingletonScope();
            // Убираем старый GameService для пятнашек
            // Bind<IGameService>().To<GameService>().InSingletonScope();

            Bind<IUserRepository>().To<UserRepositoryEF>().InSingletonScope();
            Bind<IGameRepository>().To<GameRepositoryEF>().InSingletonScope();

            // Добавляем Game2048Service
            Bind<Game2048Service>().ToSelf().InSingletonScope();
        }
    }
}