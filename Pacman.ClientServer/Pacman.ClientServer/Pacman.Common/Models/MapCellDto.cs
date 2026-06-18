using Pacman.Common.Enums;

namespace Pacman.Common.Models
{
    public class MapCellDto
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public CellType CellType { get; set; }
    }
}