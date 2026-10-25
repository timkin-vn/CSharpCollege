using Blackjack.Common.Models;
using System;
using System.Collections.Generic;

namespace Blackjack.Business.Services
{
    public static class DeckFactory
    {
        // Перемешанная колода из 52 карт
        public static List<Card> CreateShuffled()
        {
            var cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card { Suit = suit, Rank = rank });
                }
            }

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                var temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }

            return cards;
        }
    }
}
