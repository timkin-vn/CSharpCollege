using TicTacToe.Business.Models;

namespace TicTacToe.Web.Models
{
    public class GameViewModel
    {
        public CellValue[,] Cells { get; set; } = null!;
        public CellValue CurrentPlayer { get; set; }
        public GameResult Result { get; set; }
        public string Message { get; set; } = null!;
        public bool IsGameOver { get; set; }
    }
}
