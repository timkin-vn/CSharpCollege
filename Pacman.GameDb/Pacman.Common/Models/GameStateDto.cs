using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Common.Models
{
    public class GameStateDto
    {
        public GameDto Game { get; set; }
        public MapDto Map { get; set; }
        public List<GameActorDto> Actors { get; set; }
        public List<GameCollectibleStateDto> CollectibleStates { get; set; }
    }
}
