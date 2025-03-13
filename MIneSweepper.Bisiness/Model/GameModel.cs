using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIneSweepper.Bisiness.Model
{
    public class GameModel
    {
       public const int RowCount = 9;

        public const int ColumnCount = 9;
        public int RedFlags { get; set; }

        public int MineCount { get; set; }
        public int CountRevealed { get; set; }

        private CellsModel[,] _MineCells=new CellsModel[RowCount, ColumnCount];


        public CellsModel this[int rows,int colums]
        {
            get => _MineCells[rows, colums];
            internal set => _MineCells[rows, colums] = value;
        }
        
    }
}
