using System;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public bool HasWon()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount; c++)
                    if (_cells[r, c] == 2048)
                        return true;
            return false;
        }

        public bool HasMoves()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount; c++)
                    if (_cells[r, c] == 0) return true;

            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount - 1; c++)
                    if (_cells[r, c] == _cells[r, c + 1]) return true;

            for (int c = 0; c < ColumnCount; c++)
                for (int r = 0; r < RowCount - 1; r++)
                    if (_cells[r, c] == _cells[r + 1, c]) return true;

            return false;
        }
    }
}
