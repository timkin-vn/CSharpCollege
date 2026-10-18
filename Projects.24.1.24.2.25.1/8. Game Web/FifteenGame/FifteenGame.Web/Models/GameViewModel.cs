
using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;

        public int ColumnCount => GameModel.ColumnCount;

        public int MoveCount { get; set; }

        public CellViewModel[,] Cells { get; set; } =
            new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
    }
}
