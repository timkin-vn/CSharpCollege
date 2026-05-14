using Ninject;

namespace Minesweeper.Common.Infrastructure
{
    public static class NinjectKernel
    {
        public static IKernel Instance { get; set; }
    }
}