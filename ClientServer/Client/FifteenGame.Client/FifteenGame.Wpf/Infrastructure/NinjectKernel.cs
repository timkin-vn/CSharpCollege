using Ninject;
using System.Reflection;

namespace FifteenGame.Wpf.Infrastructure
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
                   
                    _kernel.Load(new FifteenGameModule());
                }
                return _kernel;
            }
        }
    }
}