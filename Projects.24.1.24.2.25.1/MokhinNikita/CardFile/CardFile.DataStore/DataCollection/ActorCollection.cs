using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.Dtos;

namespace CardFile.DataStore.DataCollection
{
    public class ActorCollection
    {
        private readonly List<ActorDto> _actors = new List<ActorDto>();
        public ActorCollection()
        {
            _actors.Add(new ActorDto());
        }
    }
}
