using MemoryGame.Business.Models;

namespace MemoryGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;

        public int ColumnCount => GameModel.ColumnCount;

        public int PairCount => GameModel.PairCount;

        public int MoveCount { get; set; }

        public int MatchedPairCount { get; set; }

        public bool IsGameOver { get; set; }

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
    }
}
