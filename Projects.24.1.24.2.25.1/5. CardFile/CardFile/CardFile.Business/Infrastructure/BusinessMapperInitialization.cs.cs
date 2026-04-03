using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;

namespace CardFile.Business.Infrastructure
{
    public static class BusinessMapperInitialization
    {
        public static void PreRegister()
        {
            // Регистрируем маппинги DataStore (чтобы CardDto <-> JsonCard и т.д.)
            CardFile.DataStore.Infrastructure.MapperInitialization.PreRegister();

            // Регистрируем собственные маппинги бизнес-слоя
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CardDto, Card>();
            cfg.CreateMap<Card, CardDto>();
        }
    }
}