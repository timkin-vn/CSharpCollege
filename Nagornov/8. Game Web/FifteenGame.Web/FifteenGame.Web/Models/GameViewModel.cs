using System.Collections.Generic;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public List<CellViewModel> Cells { get; set; } = new List<CellViewModel>();
        public int RowCount { get; set; } = 10;
        public int ColumnCount { get; set; } = 10;
        public int MinesCount { get; set; } = 15;
        public int TotalMines { get; set; }
        public int FlagsCount { get; set; }
        public string GameStatus { get; set; } = "Игра начата!";
        public bool IsGameOver { get; set; }
        public string StatusColor { get; set; } = "black";
    }

    public enum GameState
    {
        Playing,
        Won,
        GameOver
    }
}