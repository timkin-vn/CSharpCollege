using System;

namespace FrogGame.Business.Models
{
    public class GameField
    {
        public const int RowCount = 8;
        public const int ColumnCount = 8;

        private CellType[,] _cells = new CellType[RowCount, ColumnCount];

        public CellType this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int FrogRow { get; set; }
        public int FrogColumn { get; set; }
        public int HomeRow { get; set; }
        public int HomeColumn { get; set; }
        public int MovesCount { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWin { get; set; }
        public int? SelectedLilyPadRow { get; set; }
        public int? SelectedLilyPadColumn { get; set; }
    }
}