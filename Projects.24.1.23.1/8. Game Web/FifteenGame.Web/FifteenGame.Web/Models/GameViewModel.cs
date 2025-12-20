using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public CellViewModel[,] Cells { get; set; }

        public int MatchesCount { get; set; }
        public bool IsFinished { get; set; }
    }
}