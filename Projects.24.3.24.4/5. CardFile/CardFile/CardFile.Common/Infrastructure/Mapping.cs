using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Common.Infrastructure
{
    public static class Mapping
    {
        public static Mapper Mapper { get; private set; }

        public static Action<IMapperConfigurationExpression> InitializeAction;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => InitializeAction?.Invoke(cfg), NullLoggerFactory.Instance);
            Mapper = new Mapper(config);
        }
    }
}
