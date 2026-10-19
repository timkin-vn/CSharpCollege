using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister()
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
