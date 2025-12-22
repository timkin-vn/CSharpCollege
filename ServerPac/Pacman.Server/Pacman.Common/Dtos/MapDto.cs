using Pacman.Common.Enums;

namespace Pacman.Common.Dtos
{
    public class MapDto
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public CellType[] Cells { get; set; }
    }
}