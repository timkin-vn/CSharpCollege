using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.WPF.ViewModels;

namespace CardFile.WPF.Infrastructure
{
    public static class MapperInitialization
    {
        public static void Preregister()
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
