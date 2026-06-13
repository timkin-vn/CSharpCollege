using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimerList.Business.Models;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.Dtos;

namespace LimerList.Business.Infrastructure
{
    public static class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LimerItem, LimerDto>();
            cfg.CreateMap<LimerDto, LimerItem>();
        } 
    }
}
