using System;

namespace Minesweeper.Common.BusinessModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int GamesWon { get; set; }

        public double WinRate
        {
            get
            {
                return TotalGamesPlayed > 0 ? (GamesWon * 100.0) / TotalGamesPlayed: 0;
            }
        }
    }
}