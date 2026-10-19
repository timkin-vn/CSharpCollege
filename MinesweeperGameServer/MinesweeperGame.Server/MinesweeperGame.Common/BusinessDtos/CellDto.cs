using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.Common.BusinessDtos
{
    public class CellDto
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public int AdjacentMinesCount { get; set; }
        public CellState State { get; set; }
    }
}
