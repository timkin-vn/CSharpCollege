using System;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        public const int EmptyCellValue = 0;

        public const int WinTileValue = 2048;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int Score { get; internal set; }

        public bool HasWon { get; internal set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }
    }
}
