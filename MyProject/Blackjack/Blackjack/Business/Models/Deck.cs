using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame.Business.Models
{
    public class Deck
    {
        private List<Card> _cards;
        private Random _random = new Random();

        public Deck()
        {
            _cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card { Suit = suit, Rank = rank });
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = _cards[i];
                _cards[i] = _cards[j];
                _cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Колода пуста!");

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }
    }
}