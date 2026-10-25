using Blackjack.Common.Models;

namespace Blackjack.Wpf.ViewModels
{
    public class CardViewModel
    {
        public string ShortName { get; set; }

        public string Color { get; set; }

        public static CardViewModel FromCard(Card card)
        {
            return new CardViewModel
            {
                ShortName = card.ShortName,
                Color = card.IsRed ? "Red" : "Black",
            };
        }

        // Закрытая карта дилера
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
