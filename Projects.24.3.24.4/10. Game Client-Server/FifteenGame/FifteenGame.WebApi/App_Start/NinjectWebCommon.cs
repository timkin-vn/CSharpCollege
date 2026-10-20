using FifteenGame.Common.Infrastructure;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;

namespace FifteenGame.WebApi
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

                NinjectKernel.Instance = kernel;

                kernel.Load(new FifteenGameModule());

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