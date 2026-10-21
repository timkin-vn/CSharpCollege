namespace BlackjackMVC.Models
{
    public class Deck
    {
        private static readonly string[] Suits = { "♥", "♦", "♣", "♠" };
        private static readonly string[] Ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        // Новая полная колода из 52 карт
        public static List<Card> CreateCards()
        {
            var cards = new List<Card>();
            foreach (var suit in Suits)
            {
                foreach (var rank in Ranks)
                {
                    cards.Add(new Card { Suit = suit, Rank = rank });
                }
            }
            return cards;
        }

        // Берёт случайную карту из оставшихся в колоде (карта из колоды удаляется)
        public static Card Draw(List<Card> cards)
        {
            if (cards.Count == 0)
            {
                throw new InvalidOperationException("Колода пуста!");
            }

            int index = Random.Shared.Next(cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }
    }
}
