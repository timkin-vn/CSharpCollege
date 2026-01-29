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
        private readonly int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }
        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get { return _cells[row, column]; }
            set { _cells[row, column] = value; }
        }

        public int MoveCount { get; set; }
        public int Score { get; set; }
        public bool IsWin { get; set; }
        public bool IsLose { get; set; }
    }
}