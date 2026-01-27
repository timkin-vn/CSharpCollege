using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount
        {
            get { return GameModel.RowCount; }
        }

        public int ColumnCount
        {
            get { return GameModel.ColumnCount; }
        }

       
        public int Score { get; set; }

        
        public bool IsWin { get; set; }
        public bool IsLose { get; set; }

       
        public CellViewModel[,] Cells { get; set; }
            = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
    }
}