using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public List<CellModel> Cells { get; set; }

        public int MovesCount { get; set; }

        public int Seconds { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? PauseTime { get; set; }

        public int BestMoves { get; set; } = int.MaxValue;

        public int BestSeconds { get; set; } = int.MaxValue;

        public bool IsCheckingPair { get; set; }

        public bool IsGameOver { get; set; }

        public bool IsNewRecord { get; set; }

        public bool IsPreview { get; set; }

        public bool IsPaused { get; set; }

        public int FirstOpenedCell { get; set; } = -1;

        public int SecondOpenedCell { get; set; } = -1;

        public GameModel()
        {
            Cells = new List<CellModel>();
        }

        public int RowCount { get; set; } = 2;

        public int ColumnCount { get; set; } = 5;

        public string DifficultyName { get; set; } = "Легкая";
    }
}
