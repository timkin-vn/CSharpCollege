using Ninject;

namespace Pacman.Common
{
    public static class NinjectKernel
    {
        private static IKernel _kernel;

        public static IKernel Instance
        {
            get
            {
                return _kernel;
            }
        }

        public static void Initialize(IKernel kernel)
        {
            _kernel = kernel;
        }
    }
}
