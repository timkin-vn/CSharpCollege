using Ninject;
using System.Reflection;

namespace Pacman.Client.Infrastructure
{
    public static class NinjectKernel
    {
        private static StandardKernel _kernel;
        public static StandardKernel Instance
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    _kernel.Load(new ClientModule());
                }
                return _kernel;
            }
        }
    }
}