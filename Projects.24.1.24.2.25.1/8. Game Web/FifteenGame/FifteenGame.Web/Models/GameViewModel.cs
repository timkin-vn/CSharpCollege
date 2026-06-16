using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int Size
        {
            get { return GameModel.Size; }
        }

        public int[,] Matrix { get; set; } = new int[GameModel.Size, GameModel.Size];
        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWin { get; set; }
    }
}