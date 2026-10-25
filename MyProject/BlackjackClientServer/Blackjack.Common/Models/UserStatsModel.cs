namespace Blackjack.Common.Models
{
    public class UserStatsModel
    {
        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Draws { get; set; }

        public int Total => Wins + Losses + Draws;
    }
}
