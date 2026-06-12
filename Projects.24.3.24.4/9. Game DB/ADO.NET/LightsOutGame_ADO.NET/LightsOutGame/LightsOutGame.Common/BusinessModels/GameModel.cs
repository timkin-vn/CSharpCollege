using LightsOutGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Common.BusinessModels
{
    public class GameModel
    {
        private bool[,] _cells = new bool[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public bool this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int MoveCount { get; set; }
    }
}
