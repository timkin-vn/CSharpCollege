using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameField.RowCount;

        public int ColumnCoun => GameField.ColumnCount;

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameField.RowCount, GameField.ColumnCount];
    }
}