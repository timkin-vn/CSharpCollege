using Minesweeper.Business;

namespace Minesweeper.Web.Models
{

    public class GameViewModel
    {
        public Cell[,] Field { get; set; }
        public string Status { get; set; }
        public int MinesRemaining { get; set; }
        public int MoveCount { get; set; }
        public bool Finished { get; set; }
        public int Size { get { return GameModel.Size; } }
    }
}
