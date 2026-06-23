using Ninject;
using Ninject.Modules;

namespace Minesweeper.Common
{

    public static class NinjectKernel
    {
        private static IKernel _kernel;

        public static IKernel Kernel
        {
            get { return _kernel; }
        }

        public static void Initialize(params INinjectModule[] modules)
        {
            _kernel = new StandardKernel(modules);
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
