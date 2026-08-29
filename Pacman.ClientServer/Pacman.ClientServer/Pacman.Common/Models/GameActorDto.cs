using Pacman.Common.Enums;

namespace Pacman.Common.Models
{
    public class GameActorDto
    {
        public ActorType ActorType { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Direction Direction { get; set; }
        public int FrightenedTicksLeft { get; set; }
    }
}