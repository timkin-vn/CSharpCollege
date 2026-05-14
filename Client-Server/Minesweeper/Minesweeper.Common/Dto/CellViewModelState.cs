using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class CellViewModelState
    {
        public int Value { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsMine { get; set; }
        public bool IsFlagged { get; set; }
        public string DisplayText { get; set; }
        public string BackgroundColorHex { get; set; }
        public string ForegroundColorHex { get; set; }
    }
}
