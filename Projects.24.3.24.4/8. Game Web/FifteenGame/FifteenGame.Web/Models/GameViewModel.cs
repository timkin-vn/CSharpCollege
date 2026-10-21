using System.Collections.Generic;
namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public List<CellViewModel> Cells { get; set; }

        public int MovesCount { get; set; }

        public int Seconds { get; set; }

        public int BestMoves { get; set; }

        public int BestSeconds { get; set; }

        public bool IsGameOver { get; set; }

        public bool IsNewRecord { get; set; }

        public bool IsPreview { get; set; }

        public bool IsPaused { get; set; }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public string DifficultyName { get; set; }

        public GameViewModel()
        {
            Cells = new List<CellViewModel>();
        }
    }
}