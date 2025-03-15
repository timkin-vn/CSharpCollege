
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace FifteenGame.Business.Models
{

    public class GameModel
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        private string[,] _cells = new string[RowCount, ColumnCount];


        public string this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }
       
        public int[] OneRowCol = { 4, 4 }, TwoRowCol = {4,4};
        public string Onebuuton,Twobuuton;
        public int Fisra=0;

        public int  lower = 0;



    }
}
