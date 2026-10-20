using AutoMapper;
using System;

namespace CardFile.Common.Infrastructure
{
    public static class Mapping
    {
        public static IMapper Mapper { get; private set; }

        public static Action<IMapperConfigurationExpression> InitializationAction;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                InitializationAction?.Invoke(cfg);
            });

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }
    }
}
