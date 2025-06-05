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
            cfg.CreateMap<XmlCard, CardProductsDto>();
            cfg.CreateMap<CardProductsDto, XmlCard>();

            cfg.CreateMap<JsonCard, CardProductsDto>();
            cfg.CreateMap<CardProductsDto, JsonCard>();

            cfg.CreateMap<CardProductsDto, CardProductsDto>();
        }
    }
}
