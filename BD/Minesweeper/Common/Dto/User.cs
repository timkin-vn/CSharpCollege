using System;

namespace Minesweeper.Common.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }
}