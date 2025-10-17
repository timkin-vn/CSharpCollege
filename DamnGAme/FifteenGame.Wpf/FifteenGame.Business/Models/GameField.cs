using System;

namespace FifteenGame.Business.Models
{
    public class GameField
    {
        public const int RowCount = 10;         // Количество строк
        public const int ColumnCount = 10;      // Количество столбцов

        private char[,] _cells = new char[RowCount, ColumnCount]; // Массив клеток (' ' - пусто, 'S' - корабль, 'H' - попадание, 'M' - промах)

        public char this[int row, int column]    // Индексатор для доступа к клеткам
        {
            get { return _cells[row, column]; }
            internal set { _cells[row, column] = value; }
        }

        public GameField()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    _cells[row, column] = ' '; // Пустая вода
                }
            }
        }
    }
}