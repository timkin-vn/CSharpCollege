[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Pacman.Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Pacman.Server.App_Start.NinjectWebCommon), "Stop")]

namespace Pacman.Server.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using Pacman.Business.Services;
    using Pacman.Common.Interfaces.Repositories;
    using Pacman.Common.Interfaces.Services;
    using Pacman.DataAccess.EF.Repositories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                // Настройка Web API DependencyResolver
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            // Регистрация репозиториев (они создают DbContext сами)
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IGameRepository>().To<GameRepository>();
            kernel.Bind<IMapRepository>().To<MapRepository>();
            kernel.Bind<ILeaderboardRepository>().To<LeaderboardRepository>();

            // Регистрация сервисов
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<ILeaderboardService>().To<LeaderboardService>();
        }
    }
}
