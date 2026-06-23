using System;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using FifteenGame.Server.Infrastructure; 

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FifteenGame.Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FifteenGame.Server.App_Start.NinjectWebCommon), "Stop")]

namespace FifteenGame.Server.App_Start
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

                
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                
                kernel.Load(new FifteenGameServerModule());

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}