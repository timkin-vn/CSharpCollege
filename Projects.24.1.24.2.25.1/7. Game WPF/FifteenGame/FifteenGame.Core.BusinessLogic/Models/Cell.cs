namespace FifteenGame.Business.Models
{
    public class Cell
    {
        public CellType Type { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Cell(CellType type, int row, int col)
        {
            Type = type;
            Row = row;
            Column = col;
        }
    }
}