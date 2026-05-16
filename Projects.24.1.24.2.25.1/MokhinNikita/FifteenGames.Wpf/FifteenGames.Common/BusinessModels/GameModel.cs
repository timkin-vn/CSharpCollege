using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FifteenGames.Common.Definitions.Constants;
namespace FifteenGames.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[RowCount, ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }
        public int MoveCount { get; set; }
        public int FreeCellRow { get; set; }
        public int FreeCellColumn { get; set; }
    }
}
