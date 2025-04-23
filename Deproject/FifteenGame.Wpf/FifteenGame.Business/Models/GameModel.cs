using System;
using System.Collections.Generic;

namespace FifteenGame.Models
{
    public class Cell
    {
        public bool IsMine { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFlagged { get; set; }
        public int NeighborMines { get; set; }
    }

    public class GameModel
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly int _mines;
        private readonly Cell[,] _field;
        private readonly Random _random;

        public GameModel(int rows, int columns, int mines)
        {
            _rows = rows;
            _columns = columns;
            _mines = mines;
            _field = new Cell[rows, columns];
            _random = new Random();

            // Инициализация поля
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _field[i, j] = new Cell();
                }
            }
        }

        public Cell[,] Field => _field;

        public void GenerateMines(int firstRow, int firstCol)
        {
            int placedMines = 0;
            while (placedMines < _mines)
            {
                int row = _random.Next(_rows);
                int col = _random.Next(_columns);

                // не ставим мину на первую открытую ячейку и вокруг неё
                if (Math.Abs(row - firstRow) <= 1 && Math.Abs(col - firstCol) <= 1)
                    continue;

                if (!_field[row, col].IsMine)
                {
                    _field[row, col].IsMine = true;
                    placedMines++;
                }
            }

            // Подсчитываем количество мин вокруг каждой ячейки
            CalculateNeighborMines();
        }

        private void CalculateNeighborMines()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (_field[i, j].IsMine) continue;

                    int mineCount = 0;
                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            int ni = i + di;
                            int nj = j + dj;
                            if (ni >= 0 && ni < _rows && nj >= 0 && nj < _columns && _field[ni, nj].IsMine)
                            {
                                mineCount++;
                            }
                        }
                    }
                    _field[i, j].NeighborMines = mineCount;
                }
            }
        }

        public void OpenCell(int row, int col, Action<int, int> openCallback, Action updateDisplay)
        {
            if (row < 0 || row >= _rows || col < 0 || col >= _columns || _field[row, col].IsOpen || _field[row, col].IsFlagged)
                return;

            _field[row, col].IsOpen = true;
            updateDisplay?.Invoke(); // Обновляем отображение после каждого открытия

            if (_field[row, col].NeighborMines == 0 && !_field[row, col].IsMine)
            {
                for (int di = -1; di <= 1; di++)
                {
                    for (int dj = -1; dj <= 1; dj++)
                    {
                        int ni = row + di;
                        int nj = col + dj;
                        if (ni >= 0 && ni < _rows && nj >= 0 && nj < _columns)
                        {
                            openCallback(ni, nj); // Рекурсивно открываем соседние ячейки
                        }
                    }
                }
            }
        }
    }
}