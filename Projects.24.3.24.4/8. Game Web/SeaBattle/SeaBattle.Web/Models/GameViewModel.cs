using SeaBattle.Business;

namespace SeaBattle.Web.Models
{
    public class GameViewModel
    {
        public CellState[,] PlayerCells { get; set; }
        public CellState[,] EnemyCells { get; set; }
        public int MoveCount { get; set; }
        public string Status { get; set; }
        public bool Finished { get; set; }
        public int Size { get { return Board.Size; } }
    }
}
