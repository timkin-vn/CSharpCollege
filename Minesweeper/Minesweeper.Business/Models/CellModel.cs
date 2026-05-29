using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Business.Models
{
    public class CellModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMinesCount { get; set; }

        public CellModel(int row, int column)
        {
            Row = row;
            Column = column;
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
            AdjacentMinesCount = 0;
        }

        public void Reveal()
        {
            if (!IsFlagged)
            {
                IsRevealed = true;
            }
        }

        public void ToggleFlag()
        {
            if (!IsRevealed)
            {
                IsFlagged = !IsFlagged;
            }
        }
    }
}