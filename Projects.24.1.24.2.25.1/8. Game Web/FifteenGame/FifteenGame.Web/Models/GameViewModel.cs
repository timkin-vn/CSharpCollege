using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameModel.RowCount;
        public int ColumnCount => GameModel.ColumnCount;
        public CellViewModel[,] Cells { get; set; }

        public GameViewModel()
        {
            Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    Cells[r, c] = new CellViewModel { Row = r, Column = c, Direction = MoveDirection.Empty };
                }
            }
        }
    }
}
