using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;

namespace CardFile.DataStore.Infrastructure
{
    public static class MapperInitialization
    {
        public static void Preregister()
        {
            Mapping.InitializationAction += Configure;
        }
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CardDto, CardDto>();
            
        }
    }
}
