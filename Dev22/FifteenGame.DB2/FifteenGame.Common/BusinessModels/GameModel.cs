using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        public const int RowCount = 9;

        public const int ColumnCount = 9;
        public int RedFlags { get; set; }


        public int Id { get; set; }

        public int UserId { get; set; }
        public DateTime GameBegin { get; set; }
        public int MineCount { get; set; }
        public int CountRevealed { get; set; }

        private CellsModel[,] _MineCells;
        public GameModel()
        {
            _MineCells = new CellsModel[RowCount, ColumnCount];
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    _MineCells[row, column] = new CellsModel();
                }
            }
        }


        public CellsModel this[int rows,int colums]
        {
            get => _MineCells[rows, colums];
            set => _MineCells[rows, colums] = value;
        }
    }
}
