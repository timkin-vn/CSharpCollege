using AutoMapper;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.Infrastructure
{
    internal static class MapperInitialization
    {
        public static void PreRegister()
        {
            Mapping.InitializationAction += Configure;
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Book, BookViewModel>();
            cfg.CreateMap<BookViewModel, Book>();
            cfg.CreateMap<BookViewModel, BookViewModel>();
        }
    }
}
