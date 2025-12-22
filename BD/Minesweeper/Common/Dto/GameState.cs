using System;

namespace Minesweeper.Common.Dto
{
    public class GameState
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GameData { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public TimeSpan PlayTime { get; set; }
    }
}