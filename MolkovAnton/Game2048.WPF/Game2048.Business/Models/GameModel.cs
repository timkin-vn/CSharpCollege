using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int[,] Cells => _cells; 

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }
    }
}