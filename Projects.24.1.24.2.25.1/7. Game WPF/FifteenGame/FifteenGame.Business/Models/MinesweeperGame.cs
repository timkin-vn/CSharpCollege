using System;

namespace FifteenGame.Business.Models
{
    public class MinesweeperGame
    {
        private readonly MinesweeperCell[,] _cells;

        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public int MinesCount { get; private set; }
        public bool SafeFirstMove { get; private set; }
        public bool MinesPlaced { get; internal set; }
        public bool IsFinished { get; internal set; }
        public bool IsWon { get; internal set; }
        public int OpenedCells { get; internal set; }
        public int FlagsCount { get; internal set; }

        public MinesweeperCell this[int row, int column]
        {
            get { return _cells[row, column]; }
        }

        public MinesweeperGame(int rows, int columns, int minesCount, bool safeFirstMove)
        {
            if (rows <= 0 || columns <= 0 || minesCount < 0 || minesCount >= rows * columns)
            {
                throw new ArgumentException("Неверные размеры поля или количество мин.");
            }

            Rows = rows;
            Columns = columns;
            MinesCount = minesCount;
            SafeFirstMove = safeFirstMove;
            _cells = new MinesweeperCell[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    _cells[row, column] = new MinesweeperCell(row, column);
                }
            }
        }
    }
}
