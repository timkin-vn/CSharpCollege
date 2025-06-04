using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.Dtos;

namespace CardFile.Business.Infrastructure
{
    internal class MapperRegistrator
    {
        public static void Register()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Card, CardDto>();
            cfg.CreateMap<CardDto, Card>();
        }
    }
}