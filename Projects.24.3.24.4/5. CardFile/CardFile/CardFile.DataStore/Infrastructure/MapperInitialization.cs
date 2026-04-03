using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializeAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CardDto, CardDto>();

            cfg.CreateMap<XmlCard, CardDto>();
            cfg.CreateMap<CardDto, XmlCard>();

            cfg.CreateMap<JsonCard, CardDto>();
            cfg.CreateMap<CardDto, JsonCard>();
        }
    }
}
