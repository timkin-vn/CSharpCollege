using System;

namespace Minesweeper.Common.Dto
{
    public class GameStateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GameData { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public TimeSpan PlayTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}