using Game2048.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game2048.Web.Models
{
    public class GameViewModel
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public CellViewModel[,] Cells { get; set; }

        public int Score { get; set; }
        public bool IsGameOver { get; set; }
        public bool HasWon { get; set; }
    }
}
