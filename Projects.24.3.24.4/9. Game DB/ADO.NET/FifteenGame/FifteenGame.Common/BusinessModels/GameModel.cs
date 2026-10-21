using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int MoveCount { get; set; }

        public bool[,] Opened { get; } = new bool[Constants.RowCount, Constants.ColumnCount];

        public bool[,] Found { get; } = new bool[Constants.RowCount, Constants.ColumnCount];

        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }

        public bool IsOpened(int row, int column)
        {
            return Opened[row, column];
        }

        public bool IsFound(int row, int column)
        {
            return Found[row, column];
        }

        public void SetOpened(int row, int column, bool value)
        {
            Opened[row, column] = value;
        }

        public void SetFound(int row, int column, bool value)
        {
            Found[row, column] = value;
        }
    }
}