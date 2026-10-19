using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.Common.BusinessModels
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public int AdjacentMinesCount { get; set; }
        public CellState State { get; set; } = CellState.Hidden;

        public Cell() { }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
