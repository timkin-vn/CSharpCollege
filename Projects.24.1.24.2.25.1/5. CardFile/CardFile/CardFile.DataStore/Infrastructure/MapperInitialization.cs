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
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BookDto, BookDto>();

            cfg.CreateMap<BookDto, XmlCard>();
            cfg.CreateMap<XmlCard, BookDto>();

            cfg.CreateMap<BookDto, JsonCard>();
            cfg.CreateMap<JsonCard, BookDto>();
        }
    }
}
