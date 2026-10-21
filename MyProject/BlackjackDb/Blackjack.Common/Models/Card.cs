using System.Text.Json.Serialization;

namespace Blackjack.Common.Models
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades,
    }

    public enum Rank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }

    public class Card
    {
        public Suit Suit { get; set; }

        public Rank Rank { get; set; }

        [JsonIgnore]
        public bool IsRed => Suit == Suit.Hearts || Suit == Suit.Diamonds;

        [JsonIgnore]
        public string ShortName
        {
            get
            {
                string rank;
                switch (Rank)
                {
                    case Rank.Jack: rank = "J"; break;
                    case Rank.Queen: rank = "Q"; break;
                    case Rank.King: rank = "K"; break;
                    case Rank.Ace: rank = "A"; break;
                    default: rank = ((int)Rank).ToString(); break;
                }

                string suit;
                switch (Suit)
                {
                    case Suit.Hearts: suit = "♥"; break;
                    case Suit.Diamonds: suit = "♦"; break;
                    case Suit.Clubs: suit = "♣"; break;
                    default: suit = "♠"; break;
                }

                return rank + suit;
            }
        }
    }
}
