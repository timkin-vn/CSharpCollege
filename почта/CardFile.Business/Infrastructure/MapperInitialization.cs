using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;

namespace CardFile.Business.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister() => Mapping.InitializationAction += Configure;
        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Letter, LetterDto>();
            cfg.CreateMap<LetterDto, Letter>();
        }
    }
}