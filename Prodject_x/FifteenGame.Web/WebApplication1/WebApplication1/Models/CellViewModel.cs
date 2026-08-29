namespace MinesweeperWeb.Models
{
    public class CellViewModel
    {
        public CellViewModel(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }
        public int Col { get; }
        public bool IsOpen { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsMine { get; set; }
        public int NeighborMines { get; set; }
    }
}