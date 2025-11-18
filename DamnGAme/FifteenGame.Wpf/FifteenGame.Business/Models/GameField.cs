using System;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;

        private readonly char[,] _cells;

        public int ShipCount { get; private set; }

        public GameField()
        {
            _cells = new char[RowCount, ColumnCount];
            Clear();
        }

        public char this[int row, int column]
        {
            get => _cells[row, column];
            set
            {
                _cells[row, column] = value;
                if (value == 'S')
                    ShipCount++;
            }
        }

        public void Clear()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount; c++)
                    _cells[r, c] = ' ';
            ShipCount = 0;
        }

        public static bool IsInside(int row, int column)
        {
            return row >= 0 && row < RowCount && column >= 0 && column < ColumnCount;
        }

        public bool ShootAt(int row, int col)
        {
            if (!IsInside(row, col)) return false;

            char cell = _cells[row, col];

            if (cell == 'S')
            {
                _cells[row, col] = 'H';
                ShipCount--;
                return true;
            }
            else if (cell == ' ')
            {
                _cells[row, col] = 'M';
                return false;
            }
            return false;
        }

        public int GetRemainingShips() => ShipCount;
        public char GetCell(int row, int col) => _cells[row, col];

        
        public bool IsShipDestroyed(int row, int col)
        {
            if (_cells[row, col] != 'H') return false;

            
            int startCol = col;
            while (startCol > 0 && (_cells[row, startCol - 1] == 'S' || _cells[row, startCol - 1] == 'H'))
                startCol--;
            int endCol = col;
            while (endCol < ColumnCount - 1 && (_cells[row, endCol + 1] == 'S' || _cells[row, endCol + 1] == 'H'))
                endCol++;
            bool horizontalDestroyed = true;
            for (int c = startCol; c <= endCol; c++)
                if (_cells[row, c] == 'S') horizontalDestroyed = false;
            if (horizontalDestroyed && endCol > startCol) return true;

             
            int startRow = row;
            while (startRow > 0 && (_cells[startRow - 1, col] == 'S' || _cells[startRow - 1, col] == 'H'))
                startRow--;
            int endRow = row;
            while (endRow < RowCount - 1 && (_cells[endRow + 1, col] == 'S' || _cells[endRow + 1, col] == 'H'))
                endRow++;
            bool verticalDestroyed = true;
            for (int r = startRow; r <= endRow; r++)
                if (_cells[r, col] == 'S') verticalDestroyed = false;

            return verticalDestroyed;
        }
    }
}
