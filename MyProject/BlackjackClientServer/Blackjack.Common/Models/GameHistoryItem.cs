using System;

namespace Blackjack.Common.Models
{
    public class GameHistoryItem
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public GameStatus Status { get; set; }

        public int PlayerScore { get; set; }

        public int DealerScore { get; set; }
    }
}
