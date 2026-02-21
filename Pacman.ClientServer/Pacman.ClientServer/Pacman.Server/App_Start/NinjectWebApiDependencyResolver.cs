using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace Pacman.Server
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            // Не обрабатываем системные типы - оставляем их Web API
            if (serviceType.Namespace != null &&
                (serviceType.Namespace.StartsWith("System.Web.Http") ||
                 serviceType.Namespace.StartsWith("Microsoft")))
            {
                return null;
            }

            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // Kernel живёт всё время работы приложения
        }
    }
}
