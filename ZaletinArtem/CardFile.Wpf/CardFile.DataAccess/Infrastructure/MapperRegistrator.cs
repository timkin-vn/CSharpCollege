using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Infrastructure
{
    internal static class MapperRegistrator
    {
        public static void Register()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<XmlCard, CardDto>().ReverseMap();
            cfg.CreateMap<JsonCard, CardDto>().ReverseMap();
            cfg.CreateMap<CardDto, CardDto>();
        }
    }
}

