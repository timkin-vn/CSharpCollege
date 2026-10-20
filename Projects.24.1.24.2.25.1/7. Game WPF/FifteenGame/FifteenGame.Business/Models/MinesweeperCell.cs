namespace FifteenGame.Business.Models
{
    public class MinesweeperCell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsMine { get; internal set; }
        public bool IsOpened { get; internal set; }
        public bool IsFlagged { get; internal set; }
        public int MinesAround { get; internal set; }

        public MinesweeperCell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
