using Nonogram.Common.Definitions;
using System.Collections.Generic;

namespace Nonogram.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _solution = new int[Constants.RowCount, Constants.ColumnCount];
        private int[,] _userGrid = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MistakesCount { get; set; }

        public List<List<int>> RowClues { get; } = new List<List<int>>();
        public List<List<int>> ColumnClues { get; } = new List<List<int>>();

        public int this[int row, int column]
        {
            get => _userGrid[row, column];
            set => _userGrid[row, column] = value; // Убрали internal
        }

        public int GetSolution(int row, int column) => _solution[row, column];
        public void SetSolution(int row, int column, int value) => _solution[row, column] = value;
    }
}