using System;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public int Size { get; set; } = 9;
        public int MinesCount { get; set; } = 10;

        private Cell[,] _cells;

        public Cell this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int FlagsPlaced { get; set; }
        public int CellsOpened { get; set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }

        public void InitializeCells(int size)
        {
            Size = size;
            _cells = new Cell[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    _cells[r, c] = new Cell();
                }
            }

            FlagsPlaced = 0;
            CellsOpened = 0;
            GameOver = false;
            GameWon = false;
        }

        public int GetTotalNonMineCells()
        {
            return (Size * Size) - MinesCount;
        }
    }

    public class Cell
    {
        public bool IsMine { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public int MinesAround { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}