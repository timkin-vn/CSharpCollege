using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Business.Models
{
    public class GameCell
    {
        public bool HasMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMinesCount { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}