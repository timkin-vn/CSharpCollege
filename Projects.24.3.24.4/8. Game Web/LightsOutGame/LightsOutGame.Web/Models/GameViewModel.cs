using LightsOutGame.Business.Models;

namespace LightsOutGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;
        public int ColumnCount => GameModel.ColumnCount;

        public CellViewModel[,] Cells { get; set; } =
            new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
    }
}
