using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Nonogram.Common.Repositories;
using Nonogram.Common.Services;
using Nonogram.Business.Services;
using Nonogram.DataAccess.Repositories;
using System;
using System.Web;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Nonogram.Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Nonogram.Server.App_Start.NinjectWebCommon), "Stop")]

namespace Nonogram.Server.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
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
                Nonogram.Common.Infrastructure.NinjectKernel.Instance = kernel;

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
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IGameRepository>().To<GameRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}