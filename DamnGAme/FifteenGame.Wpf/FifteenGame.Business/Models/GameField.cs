using System;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;

        private readonly char[,] _cells;

        /// <summary>
        /// Количество кораблей, оставшихся на поле.
        /// </summary>
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

        /// <summary>
        /// Полностью очищает поле.
        /// </summary>
        public void Clear()
        {
            for (int r = 0; r < RowCount; r++)
                for (int c = 0; c < ColumnCount; c++)
                    _cells[r, c] = ' ';
            ShipCount = 0;
        }

        /// <summary>
        /// Проверяет, находится ли клетка в пределах поля.
        /// </summary>
        public static bool IsInside(int row, int column)
        {
            return row >= 0 && row < RowCount && column >= 0 && column < ColumnCount;
        }

        /// <summary>
        /// Регистрирует выстрел по клетке.
        /// </summary>
        /// <returns>
        /// true — если попали по кораблю, false — если мимо или уже было.
        /// </returns>
        public bool ShootAt(int row, int col)
        {
            if (!IsInside(row, col))
                return false;

            char cell = _cells[row, col];

            if (cell == 'S')
            {
                _cells[row, col] = 'H';
                ShipCount--; // 💥 уменьшаем количество кораблей
                return true;
            }
            else if (cell == ' ')
            {
                _cells[row, col] = 'M';
                return false;
            }

            // Если сюда попали по уже отмеченной клетке — ничего не делаем
            return false;
        }

        /// <summary>
        /// Возвращает количество оставшихся кораблей.
        /// </summary>
        public int GetRemainingShips() => ShipCount;

        /// <summary>
        /// Возвращает символ в клетке (для отрисовки в UI).
        /// </summary>
        public char GetCell(int row, int col) => _cells[row, col];
    }
}
