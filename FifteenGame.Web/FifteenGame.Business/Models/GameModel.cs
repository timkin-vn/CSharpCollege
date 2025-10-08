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
        public const int RowCount = 5;

        public const int ColumnCount = 5;

        public int PlayerValue = 0;

        public int StepsLeft = 10;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public int PlayerRow { get; internal set; }

        public int PlayerColumn { get; internal set; }
    }
}
