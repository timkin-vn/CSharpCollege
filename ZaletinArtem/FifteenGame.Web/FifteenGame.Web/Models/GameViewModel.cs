using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.Size;
        public int ColumnCount => GameModel.Size;
        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameModel.Size, GameModel.Size];
    }
}
