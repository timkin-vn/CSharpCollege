using Pacman.Common.Enums;

namespace Pacman.Common.Models
{
    public class GameCollectibleStateDto
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public CellType CollectibleType { get; set; }
        public bool IsEaten { get; set; }
    }
}