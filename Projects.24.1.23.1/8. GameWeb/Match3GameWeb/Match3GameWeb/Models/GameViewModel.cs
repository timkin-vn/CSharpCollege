using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Match3GameWeb.Models
{
    public class GameViewModel
    {
        public CellViewModel[,] Cells { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public bool IsGameOver { get; set; }
    }
}