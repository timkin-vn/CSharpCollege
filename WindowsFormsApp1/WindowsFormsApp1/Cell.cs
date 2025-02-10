using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Cell
    {
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int Number { get; set; }

        public Cell()
        {
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
            Number = 0;
        }
    }
}