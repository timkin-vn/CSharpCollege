namespace BlackjackMVC.Models
{
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();

            string[] suits =
            {
                "♥",
                "♦",
                "♣",
                "♠"
            };

            string[] ranks =
            {
                "A","2","3","4","5","6","7","8","9","10","J","Q","K"
            };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card
                    {
                        Suit = suit,
                        Rank = rank
                    });
                }
            }
        }

        public Card Draw()
        {
            int index = random.Next(cards.Count);

            Card card = cards[index];

            cards.RemoveAt(index);

            return card;
        }
    }
}