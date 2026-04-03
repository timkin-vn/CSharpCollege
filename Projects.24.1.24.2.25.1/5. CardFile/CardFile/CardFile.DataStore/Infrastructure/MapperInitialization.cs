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
    public static class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CardDto, CardDto>(); // для Clone

            // JSON
            cfg.CreateMap<JsonCard, CardDto>();
            cfg.CreateMap<CardDto, JsonCard>()
                .ForMember(dest => dest.ManufactureDateText, opt => opt.MapFrom(src => src.ManufactureDate.ToShortDateString()))
                .ForMember(dest => dest.ReceiptDateText, opt => opt.MapFrom(src => src.ReceiptDate.ToShortDateString()))
                .ForMember(dest => dest.WriteOffDateText,
                    opt => opt.MapFrom(src => src.WriteOffDate.HasValue ? src.WriteOffDate.Value.ToShortDateString() : "-"));

            // XML
            cfg.CreateMap<XmlCard, CardDto>();
            cfg.CreateMap<CardDto, XmlCard>()
                .ForMember(dest => dest.ManufactureDateText, opt => opt.MapFrom(src => src.ManufactureDate.ToShortDateString()))
                .ForMember(dest => dest.ReceiptDateText, opt => opt.MapFrom(src => src.ReceiptDate.ToShortDateString()))
                .ForMember(dest => dest.WriteOffDateText,
                    opt => opt.MapFrom(src => src.WriteOffDate.HasValue ? src.WriteOffDate.Value.ToShortDateString() : "-"));
        }
    }
}