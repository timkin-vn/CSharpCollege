using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => 8;  

        public int ColumnCount => 8; 

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[8, 8];

        public string GameStatus { get; set; }

        public bool IsGameOver { get; set; }

        public bool HasSelectedLilyPad { get; set; }

        public int MovesCount { get; set; }
    }
}