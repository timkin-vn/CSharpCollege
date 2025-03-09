using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.StorageEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Infrastructure
{
    internal static class MapperInitialize
    {
        public static void Initialize()
        {
            Mapping.Initialization += Initialize;
        }

        private static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<XmlCard, CardDto>();
            cfg.CreateMap<CardDto, XmlCard>();

            cfg.CreateMap<JsonCard, CardDto>();
            cfg.CreateMap<CardDto, JsonCard>();

            cfg.CreateMap<CardDto, CardDto>();
        }
    }
}
