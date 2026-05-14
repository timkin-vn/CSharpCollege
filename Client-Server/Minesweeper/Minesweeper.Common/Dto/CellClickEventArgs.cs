using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class CellClickEventArgs : EventArgs
    {
        public CellClickEventArgs(CellPosition position)
        {
            Position = position;
        }

        public CellPosition Position { get; }
    }
}
