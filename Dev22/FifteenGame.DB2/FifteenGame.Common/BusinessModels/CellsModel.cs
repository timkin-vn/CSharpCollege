using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class CellsModel
    {
        public bool IsMine { get; set; }
        public int NeightborMines { get; set; }
        public bool IsRevealed { get; set; }
        public bool Isflag { get; set; }

        public CellsModel()
        {
            IsMine = false;
            NeightborMines = 0;
            IsRevealed = false;
            Isflag = false;
        }


    }
}
