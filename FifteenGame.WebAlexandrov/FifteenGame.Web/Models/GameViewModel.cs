using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using static FifteenGame.Business.Models.GameModel;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;

        public int ColumnCount => GameModel.ColumnCount;

        public CellViewModel[,] Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];

        public int Property1 { get; set; }
        public string Property2 { get; set; }

        public GameViewModel() { }
        public UnitType Type { get; set; }


    }
}