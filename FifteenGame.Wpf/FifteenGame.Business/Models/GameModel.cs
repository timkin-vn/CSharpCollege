using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;

       
        public const int EmptyCellValue = 0;

        private readonly int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public int Score { get; internal set; }

        public bool IsWin { get; internal set; }  
        public bool IsLose { get; internal set; }  
    }
}