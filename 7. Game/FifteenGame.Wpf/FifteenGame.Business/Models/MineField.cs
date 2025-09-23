using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Business.Models
{
    public class MineField
    {
        public const int DefaultRows = 10;
        public const int DefaultColumns = 10;
        public const int DefaultMines = 15;

        public int Rows { get; set; } = DefaultRows;
        public int Columns { get; set; } = DefaultColumns;
        public int MineCount { get; set; } = DefaultMines;

        public GameCell[,] Cells { get; set; }
        public GameState State { get; set; } = GameState.NotStarted;
        public int RevealedCellsCount { get; set; }
        public int FlaggedMinesCount { get; set; }
    }
}