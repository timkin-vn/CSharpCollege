using System;

namespace FifteenGame.Common.Models
{
    public enum CellState { Empty, X, O }
    public enum GameResult { None, PlayerWin, ComputerWin, Draw }

    public class GameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BoardState { get; set; } = "         ";
        public GameResult Result { get; set; } = GameResult.None;
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }
    }
}