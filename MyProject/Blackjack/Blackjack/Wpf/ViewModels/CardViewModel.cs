using BlackjackGame.Business.Models;

namespace BlackjackGame.Wpf.ViewModels
{
    public class CardViewModel
    {
        public string ShortName { get; set; }

        public string Color { get; set; }

        public static CardViewModel FromCard(Card card)
        {
            var isRed = card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds;
            return new CardViewModel
            {
                ShortName = card.ShortName,
                Color = isRed ? "Red" : "Black",
            };
        }

        // Рубашка карты (закрытая карта дилера)
        public static CardViewModel Hidden()
        {
            return new CardViewModel
            {
                ShortName = "?",
                Color = "Gray",
            };
        }
    }
}
