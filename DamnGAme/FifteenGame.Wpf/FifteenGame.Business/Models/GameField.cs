using System;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int Size = 10;
        private readonly char[,] _cells = new char[Size, Size];
        public int ShipCount { get; private set; }

        public GameField()
        {
            Clear();
        }

        public char this[int row, int col]
        {
            get { return _cells[row, col]; }
            set
            {
                if (_cells[row, col] == 'S' && value != 'S') ShipCount--;
                if (value == 'S' && _cells[row, col] != 'S') ShipCount++;
                _cells[row, col] = value;
            }
        }

        public void Clear()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    _cells[r, c] = ' ';
            ShipCount = 0;
        }

        public static bool IsValid(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        public bool Shoot(int row, int col)
        {
            if (!IsValid(row, col)) return false;
            if (_cells[row, col] == 'H' || _cells[row, col] == 'M') return false;

            if (_cells[row, col] == 'S')
            {
                _cells[row, col] = 'H';
                ShipCount--;
                return true;
            }

            _cells[row, col] = 'M';
            return false;
        }

        public bool IsShipDestroyed(int row, int col)
        {
            if (_cells[row, col] != 'H') return false;

            // Горизонталь
            int left = col;
            while (left > 0 && (_cells[row, left - 1] == 'S' || _cells[row, left - 1] == 'H')) left--;
            int right = col;
            while (right < Size - 1 && (_cells[row, right + 1] == 'S' || _cells[row, right + 1] == 'H')) right++;

            bool horDestroyed = true;
            for (int c = left; c <= right; c++)
                if (_cells[row, c] == 'S') horDestroyed = false;
            if (horDestroyed && right >= left) return true;

            // Вертикаль
            int up = row;
            while (up > 0 && (_cells[up - 1, col] == 'S' || _cells[up - 1, col] == 'H')) up--;
            int down = row;
            while (down < Size - 1 && (_cells[down + 1, col] == 'S' || _cells[down + 1, col] == 'H')) down++;

            bool vertDestroyed = true;
            for (int r = up; r <= down; r++)
                if (_cells[r, col] == 'S') vertDestroyed = false;

            return vertDestroyed;
        }

        public void PlaceShip(int r, int c, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                if (horizontal)
                    this[r, c + i] = 'S';
                else
                    this[r + i, c] = 'S';
            }
        }

        public bool CanPlaceShip(int r, int c, int size, bool horizontal)
        {
            if (horizontal && c + size > Size) return false;
            if (!horizontal && r + size > Size) return false;

            for (int i = -1; i <= size; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nr = horizontal ? r + j : r + i;
                    int nc = horizontal ? c + i : c + j;
                    if (IsValid(nr, nc) && _cells[nr, nc] == 'S')
                        return false;
                }
            }
            return true;
        }

        public int GetRemainingShips() => ShipCount;
    }
}