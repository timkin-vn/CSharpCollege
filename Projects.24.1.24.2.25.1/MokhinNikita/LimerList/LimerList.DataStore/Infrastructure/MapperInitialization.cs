using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.FileAccess.Entities;

namespace LimerList.DataStore.Infrastructure
{
    public class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LimerDto, LimerDto>();
            cfg.CreateMap<LimerDto, XmlLimer>();
            cfg.CreateMap<XmlLimer, LimerDto>();
            cfg.CreateMap<LimerDto, JsonLimer>();
            cfg.CreateMap<JsonLimer, LimerDto>();
        }
    }
}
