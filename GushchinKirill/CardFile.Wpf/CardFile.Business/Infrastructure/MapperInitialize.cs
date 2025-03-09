using AutoMapper;
using CardFile.Business.Entities;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Infrastructure
{
    internal static class MapperInitialize
    {
        public static void Initialize()
        {
            Mapping.Initialization += Initialize;
        }

        private static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Card, CardDto>();
            cfg.CreateMap<CardDto, Card>();
        }
    }
}
