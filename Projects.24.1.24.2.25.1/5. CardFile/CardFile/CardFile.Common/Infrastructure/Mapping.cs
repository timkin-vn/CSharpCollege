using AutoMapper;
using System;

namespace CardFile.Common.Infrastructure
{
    public static class Mapping
    {
        public static IMapper Mapper { get; private set; }

        public static Action<IMapperConfigurationExpression> InitializeAction { get; set; }

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                InitializeAction?.Invoke(cfg);
            });

            Mapper = config.CreateMapper();
        }
    }
}