using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Common.Infrastructure
{
    public static class Mapping
    {
        public static Mapper Mapper { get; set; }

        public static Action<IMapperConfigurationExpression> InitializationAction;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => InitializationAction?.Invoke(cfg));
            Mapper = new Mapper(config);
        }
    }
}
