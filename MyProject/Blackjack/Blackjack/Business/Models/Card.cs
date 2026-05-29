using System;

namespace BlackjackGame.Business.Models
{
    public enum Suit
    {
        Hearts, Diamonds, Clubs, Spades
    }

    public enum Rank
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10,
        Jack = 11, Queen = 12, King = 13, Ace = 14
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public string DisplayName
        {
            get { return Rank.ToString() + " of " + Suit.ToString(); }
        }

        public string ShortName
        {
            get
            {
                string suitSymbol = "";
                switch (Suit)
                {
                    case Suit.Hearts:
                        suitSymbol = "♥";
                        break;
                    case Suit.Diamonds:
                        suitSymbol = "♦";
                        break;
                    case Suit.Clubs:
                        suitSymbol = "♣";
                        break;
                    case Suit.Spades:
                        suitSymbol = "♠";
                        break;
                }

                string rankStr = "";
                switch (Rank)
                {
                    case Rank.Jack:
                        rankStr = "J";
                        break;
                    case Rank.Queen:
                        rankStr = "Q";
                        break;
                    case Rank.King:
                        rankStr = "K";
                        break;
                    case Rank.Ace:
                        rankStr = "A";
                        break;
                    default:
                        rankStr = ((int)Rank).ToString();
                        break;
                }

                return rankStr + suitSymbol;
            }
        }
    }
}