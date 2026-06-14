using Minesweeper.Business.Models;

namespace MinesweeperWeb.Models
{
    public class GameViewModel
    {
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int MineCount { get; set; }
        public int FlagsPlaced { get; set; }
        public GameState State { get; set; }
        public int RemainingMines => MineCount - FlagsPlaced;

        public CellViewModel[,] Cells { get; set; }

        public string StatusMessage => State switch
        {
            GameState.Won => "🎉 Победа!",
            GameState.Lost => "💥 Вы проиграли!",
            _ => "Игра идёт"
        };

        public bool IsGameOver => State != GameState.Playing;
    }
}