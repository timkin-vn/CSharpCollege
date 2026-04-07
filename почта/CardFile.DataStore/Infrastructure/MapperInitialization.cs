using AutoMapper;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;

namespace CardFile.DataStore.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister() => Mapping.InitializationAction += Configure;
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<LetterDto, LetterDto>();
            cfg.CreateMap<LetterDto, XmlLetter>();
            cfg.CreateMap<XmlLetter, LetterDto>();
            cfg.CreateMap<LetterDto, JsonLetter>();
            cfg.CreateMap<JsonLetter, LetterDto>();
        }
    }
}