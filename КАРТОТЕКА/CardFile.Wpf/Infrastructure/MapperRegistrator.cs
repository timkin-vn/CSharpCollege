using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.ViewModels;

namespace CardFile.Wpf.Infrastructure
{
    internal static class MapperRegistrator
    {
        public static void Register()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Card, CardViewModel>();
            cfg.CreateMap<CardViewModel, Card>();
            cfg.CreateMap<CardViewModel, CardViewModel>();
        }
    }
}