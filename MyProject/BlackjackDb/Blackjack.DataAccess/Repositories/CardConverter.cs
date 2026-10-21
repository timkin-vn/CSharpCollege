using Blackjack.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.DataAccess.Repositories
{
    // Перевод списка карт в строку для БД и обратно
    internal static class CardConverter
    {
        public static string ToText(IEnumerable<Card> cards)
        {
            return string.Join(";", cards.Select(c => $"{(int)c.Rank}:{(int)c.Suit}"));
        }

        public static List<Card> FromText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new List<Card>();
            }

            return text
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(part =>
                {
                    var values = part.Split(':');
                    return new Card
                    {
                        Rank = (Rank)int.Parse(values[0]),
                        Suit = (Suit)int.Parse(values[1]),
                    };
                })
                .ToList();
        }
    }
}
