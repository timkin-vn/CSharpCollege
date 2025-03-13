using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 7;

        public const int ColumnCount = 7;

        private char[,] _cells = new char[RowCount, ColumnCount];

        public List<(int, int)> SelectedCells { get; set; } = new List<(int, int)>();

        public char this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }
    }
}