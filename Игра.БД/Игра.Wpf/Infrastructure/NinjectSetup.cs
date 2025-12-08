using Игра.Common.Infrastructure;
using Ninject;
using System;

namespace Игра.Wpf.Infrastructure
{
    public static class NinjectSetup
    {
        public static void Init()
        {
            if (NinjectKernel.Instance == null)
            {
                var kernel = new StandardKernel(new ИграModule());
                NinjectKernel.Instance = kernel;
            }
        }
    }
}