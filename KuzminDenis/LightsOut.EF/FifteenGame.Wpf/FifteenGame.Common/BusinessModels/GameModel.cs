using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        public const int RowCount = 5;
        public const int ColumnCount = 5;
        private bool[,] _cells = new bool[RowCount, ColumnCount];

        public bool this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int MaxMoves { get; set; } = 50;
    }
}