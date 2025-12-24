using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Infrastructure
{
    public static class NinjectKernel
    {
        public static IKernel Instance { get; set; }
    }
}
