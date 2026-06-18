using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Pacman.Common
{
    public static class NinjectKernel
    {
        private static IKernel _instance;

        public static IKernel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StandardKernel();
                }
                return _instance;
            }
        }

        public static void Initialize(IKernel kernel)
        {
            _instance = kernel;
        }
    }
}