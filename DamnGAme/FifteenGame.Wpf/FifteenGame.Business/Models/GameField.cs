using System;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int Size = 10;
        private readonly char[,] _cells = new char[Size, Size];
        public int ShipsRemaining { get; private set; } = 0;

        public GameField() => Clear();

        public char this[int row, int col]
        {
            get => _cells[row, col];
            private set
            {
                if (_cells[row, col] == 'S' && value != 'S') ShipsRemaining--;
                if (value == 'S' && _cells[row, col] != 'S') ShipsRemaining++;
                _cells[row, col] = value;
            }
        }

        public void Clear()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    _cells[r, c] = ' ';
            ShipsRemaining = 0;
        }

        public static bool IsValid(int r, int c) => r >= 0 && r < Size && c >= 0 && c < Size;

        public bool Shoot(int row, int col)
        {
            if (!IsValid(row, col)) return false;
            if (_cells[row, col] is 'H' or 'M') return false;

            if (_cells[row, col] == 'S')
            {
                this[row, col] = 'H';
                return true;
            }

            this[row, col] = 'M';
            return false;
        }

        public bool IsShipSunken(int row, int col)
        {
            if (_cells[row, col] != 'H') return false;

            // Проверяем горизонтальный корабль
            if (CheckDirection(row, col, 0, 1) || CheckDirection(row, col, 0, -1))
                return true;

            // Проверяем вертикальный корабль
            if (CheckDirection(row, col, 1, 0) || CheckDirection(row, col, -1, 0))
                return true;

            return false; // одиночный корабль потоплен сразу
        }

        private bool CheckDirection(int r, int c, int dr, int dc)
        {
            int len = 0;
            while (IsValid(r, c) && (_cells[r, c] == 'H' || _cells[r, c] == 'S'))
            {
                if (_cells[r, c] == 'S') return false;
                len++;
                r += dr; c += dc;
            }
            return len > 0;
        }

        public void PlaceShip(int r, int c, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int row = horizontal ? r : r + i;
                int col = horizontal ? c + i : c;
                this[row, col] = 'S';
            }
        }

        public bool CanPlaceShip(int r, int c, int size, bool horizontal)
        {
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

        public int GetRemainingShips() => ShipsRemaining;
    }
}