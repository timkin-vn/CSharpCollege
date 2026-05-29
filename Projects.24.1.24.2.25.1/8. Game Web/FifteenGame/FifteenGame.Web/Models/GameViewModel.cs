using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int Size => GameModel.Size;
        public int[,] Cells { get; set; }
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWin { get; set; }

        public GameViewModel()
        {
            Cells = new int[GameModel.Size, GameModel.Size];
        }
    }
}