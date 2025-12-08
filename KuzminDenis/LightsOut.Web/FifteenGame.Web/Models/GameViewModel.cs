using LightsOut.Business.Models;

namespace LightsOut.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;
        public int ColumnCount => GameModel.ColumnCount;

        public CellViewModel[,] Cells { get; set; } =
            new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];

        public bool IsGameOver { get; set; }
        public bool IsGameLose { get; set; }
        public int MovesLeft { get; set; }
        public int MaxMoves { get; set; }
    }
}
