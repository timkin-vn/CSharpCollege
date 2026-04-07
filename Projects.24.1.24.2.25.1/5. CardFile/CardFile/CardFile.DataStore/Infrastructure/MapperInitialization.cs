using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;

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
            cfg.CreateMap<CardDto, CardDto>(); // для Clone

            cfg.CreateMap<CardDto, XmlCard>();
            cfg.CreateMap<XmlCard, CardDto>();

            cfg.CreateMap<CardDto, JsonCard>();
            cfg.CreateMap<JsonCard, CardDto>();
        }
    }
}