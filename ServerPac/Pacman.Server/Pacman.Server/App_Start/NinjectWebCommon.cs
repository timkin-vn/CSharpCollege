[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Pacman.Server.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Pacman.Server.App_Start.NinjectWebCommon), "Stop")]

namespace Pacman.Server.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost; 
    using Ninject.Web.WebApi;
    using System.Web.Http;
    using Pacman.Server.Infrastructure; 

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

                
                kernel.Unbind<System.Web.Http.Validation.ModelValidatorProvider>();
                kernel.Unbind<System.Web.Http.Filters.IFilterProvider>();

                
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                
                kernel.Load(new PacmanModule());

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
            
            kernel.Load(new PacmanModule());
        }
    }
}