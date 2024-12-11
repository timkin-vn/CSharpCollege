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

        public static Action<IMapperConfigurationExpression> Initialization;

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg => Initialization?.Invoke(cfg));
            Mapper = new Mapper(config);
        }
    }
}
