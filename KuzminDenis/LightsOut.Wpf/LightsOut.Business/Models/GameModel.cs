using System;
using LightsOut.Business.Models;

namespace LightsOut.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 5;
        public const int ColumnCount = 5;

        private bool[,] _cells = new bool[RowCount, ColumnCount];

        public bool this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }
    }
}