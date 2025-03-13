using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.Size;

        public int ColumnCount => GameModel.Size;

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameModel.Size, GameModel.Size];
    }
}