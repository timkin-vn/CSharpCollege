using Blackjack.Common.Models;
using System.Collections.Generic;

namespace Blackjack.Common.Rules
{
    public static class ScoreCalculator
    {
        // Туз считается за 11, но если перебор — за 1
        public static int Calculate(IEnumerable<Card> cards)
        {
            int score = 0;
            int aces = 0;

            foreach (var card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aces++;
                    score += 11;
                }
                else if (card.Rank >= Rank.Ten)
                {
                    score += 10;
                }
                else
                {
                    score += (int)card.Rank;
                }
            }

            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }

            return score;
        }
    }
}
