
namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount { get { return 4; } }

        public int ColumnCount { get { return 4; } }

        public CellViewModel[,] Cells { get; set; } =
            new CellViewModel[4, 4];

        public int Score { get; set; }

        public int BestScore { get; set; }

        public bool CanUndo { get; set; }

        public bool HasWon { get; set; }

        public bool IsGameOver { get; set; }
    }
}
