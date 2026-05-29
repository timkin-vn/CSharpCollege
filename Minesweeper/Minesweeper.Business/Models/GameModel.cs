using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Business.Models
{
    public class GameModel
    {
        public const int DefaultRowCount = 10;
        public const int DefaultColumnCount = 10;
        public const int DefaultMineCount = 15;

        private CellModel[,] _cells;

        public CellModel this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        public int MineCount { get; private set; }
        public int FlagsPlaced { get; set; }
        public GameState State { get; set; }
        public bool IsFirstClick { get; set; }

        public GameModel(int rowCount = DefaultRowCount, int columnCount = DefaultColumnCount, int mineCount = DefaultMineCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            MineCount = mineCount;
            _cells = new CellModel[RowCount, ColumnCount];
            FlagsPlaced = 0;
            State = GameState.Playing;
            IsFirstClick = true;

            InitializeCells();
        }

        private void InitializeCells()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    _cells[row, column] = new CellModel(row, column);
                }
            }
        }

        public void PlaceMines(int excludeRow, int excludeColumn)
        {
            var random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < MineCount)
            {
                int row = random.Next(RowCount);
                int column = random.Next(ColumnCount);

                if (!_cells[row, column].IsMine &&
                    !(row == excludeRow && column == excludeColumn))
                {
                    _cells[row, column].IsMine = true;
                    minesPlaced++;
                }
            }

            CalculateAdjacentMines();
        }

        private void CalculateAdjacentMines()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (!_cells[row, column].IsMine)
                    {
                        _cells[row, column].AdjacentMinesCount = CountAdjacentMines(row, column);
                    }
                }
            }
        }

        private int CountAdjacentMines(int row, int column)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < RowCount &&
                        newColumn >= 0 && newColumn < ColumnCount)
                    {
                        if (_cells[newRow, newColumn].IsMine)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public bool CheckWin()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    if (!_cells[row, column].IsMine && !_cells[row, column].IsRevealed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}