using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int MoveCount { get; set; }

        public int FreeCellRow { get; set; } = -1;

        public int FreeCellColumn { get; set; } = -1;

        public int Difficulty { get; set; } = 1;

        public int Theme { get; set; } = 1;     

        public int Score { get; set; }

        public int ComboStreak { get; set; }

        public int HintsLeft { get; set; } = Constants.DefaultHintsCount;

        public int ElapsedSeconds { get; set; }
    }
}
