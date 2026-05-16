using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.ViewModels;

namespace CardFile.Wpf.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            
            cfg.CreateMap<Company, CardEditViewModel>();
            cfg.CreateMap<CardEditViewModel, Company>();
            cfg.CreateMap<CardEditViewModel, CardEditViewModel>();
        }
    }
}