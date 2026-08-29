using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.ViewModels;

namespace CardFile.Wpf.Infrastructure
{
    internal static class WpfMapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializeAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Student, StudentViewModel>();
            cfg.CreateMap<StudentViewModel, Student>();
        }
    }
}