[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Server.App_Start.NinjectWebCommon), "Stop")]

namespace Server.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Minesweeper.Common.Repositories;
    using Minesweeper.Common.Services;
    using Minesweeper.DataAccess.EF.Repositories;
    using Minesweeper.Business.Services;
    using Minesweeper.Common.Infrastructure;
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

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

                GlobalConfiguration.Configuration.DependencyResolver =
                    new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);

                NinjectKernel.Instance = kernel;

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
            kernel.Bind<IUserRepository>().To<UserRepositoryEF>().InRequestScope();
            kernel.Bind<IGameRepository>().To<GameRepositoryEF>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IGameService>().To<GameService>().InRequestScope();
        }
    }
}