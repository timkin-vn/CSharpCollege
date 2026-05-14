using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalGamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public double WinRate { get; set; }
        public DateTime? LastPlayedAt { get; set; }
    }
}
