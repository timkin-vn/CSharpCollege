using System;

namespace Minesweeper.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;
        public const int MineCount = 15;

        private readonly bool[,] _mines = new bool[RowCount, ColumnCount];
        private readonly bool[,] _revealed = new bool[RowCount, ColumnCount];
        private readonly bool[,] _flagged = new bool[RowCount, ColumnCount];
        private readonly int[,] _numbers = new int[RowCount, ColumnCount];

        public bool HasMine(int row, int column) { return _mines[row, column]; }
        internal void SetMine(int row, int column, bool value) { _mines[row, column] = value; }
        public bool IsRevealed(int row, int column) { return _revealed[row, column]; }
        internal void SetRevealed(int row, int column, bool value) { _revealed[row, column] = value; }
        public bool IsFlagged(int row, int column) { return _flagged[row, column]; }
        internal void SetFlagged(int row, int column, bool value) { _flagged[row, column] = value; }
        public int GetNumber(int row, int column) { return _numbers[row, column]; }
        internal void SetNumber(int row, int column, int value) { _numbers[row, column] = value; }

        public bool IsGameOver { get; internal set; }
        public bool IsWon { get; internal set; }
        public int FlagsCount
        {
            get
            {
                int count = 0;
                for (int r = 0; r < RowCount; r++)
                    for (int c = 0; c < ColumnCount; c++)
                        if (_flagged[r, c]) count++;
                return count;
            }
        }
    }
}
