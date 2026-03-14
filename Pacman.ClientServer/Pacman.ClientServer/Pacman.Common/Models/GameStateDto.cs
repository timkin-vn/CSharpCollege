using System.Collections.Generic;

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