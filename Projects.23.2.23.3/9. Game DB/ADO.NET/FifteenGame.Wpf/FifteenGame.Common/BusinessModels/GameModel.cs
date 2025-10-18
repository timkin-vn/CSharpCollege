using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        public const int FreeCellValue = -1;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int Id { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }

        public int MoveCount { get; set; }
    }
}
