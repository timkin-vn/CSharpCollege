using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 5;

        public const int ColumnCount = 5;

        public const int LightOffValue = 0;

        public const int LightOnValue = 1;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

    }
}
