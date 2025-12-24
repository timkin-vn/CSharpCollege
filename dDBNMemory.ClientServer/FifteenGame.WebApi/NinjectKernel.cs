using Ninject;
using Ninject.Modules;
using System;
using System.Linq;

namespace FifteenGame.Common.Infrastructure
{
    public static class NinjectKernel
    {
        private static readonly Lazy<IKernel> _instance = new Lazy<IKernel>(CreateKernel);

        public static IKernel Instance => _instance.Value;

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            // Загружаем FifteenGameModule ОДИН раз (без kernel.Load(assemblies))
            var moduleType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .FirstOrDefault(t => typeof(INinjectModule).IsAssignableFrom(t)
                                     && t.Name == "FifteenGameModule");

            if (moduleType == null)
                throw new InvalidOperationException("Не найден Ninject-модуль FifteenGameModule. Проверь что FifteenGameModule.cs есть в WebApi проекте.");

            var module = (INinjectModule)Activator.CreateInstance(moduleType);
            kernel.Load(module);

            return kernel;
        }
    }
}
