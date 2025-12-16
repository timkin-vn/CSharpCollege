using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Business.Models
{
    public class NonogramModel
    {
        public const int Size = 15;
        public const int EmptyCell = 0;
        public const int FilledCell = 1;
        public const int CrossedCell = -1;

        private int[,] _solution = new int[Size, Size];
        private int[,] _userGrid = new int[Size, Size];

        public int this[int row, int column]
        {
            get => _userGrid[row, column];
            internal set => _userGrid[row, column] = value;
        }

        public int GetSolution(int row, int column) => _solution[row, column];

        public void SetSolution(int row, int column, int value) => _solution[row, column] = value;

        public int MistakesCount { get; internal set; }

        public List<List<int>> RowClues { get; } = new List<List<int>>();
        public List<List<int>> ColumnClues { get; } = new List<List<int>>();
    }
}