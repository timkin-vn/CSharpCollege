using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimerList.Business.Models;
using LimerList.Common.Infrastructure;
using LimerList.ViewModels;


namespace LimerList.WPF.Infrastructure
{
    public class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LimerItem, LimerViewModel>();
            cfg.CreateMap<LimerViewModel, LimerItem>();
            cfg.CreateMap<LimerViewModel, LimerViewModel>();
        }
    }
}
