using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Definitions;

namespace Game2048.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int[,] Cells => _cells;

        public int GameId { get; set; }

        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }
    }
}
