using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//состояние игры
namespace MemoryGames.Model
{
    public class GameModel
    {
        public List<Card> Cards { get; private set; }
        public Card FirstFlippedCard { get; set; } 
        public int Rows { get; }
        public int Cols { get; }

        public event EventHandler GameWon;
        public event EventHandler CardsUpdated;

        public GameModel(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            InitializeGame();
        }

        private void InitializeGame()
        {
            string[] imagePaths = {
                "~/Content/images/image1.png",
                "~/Content/images/image.png",
                "~/Content/images/image3.png"
            };

            List<Card> allCards = new List<Card>();

            
            for (int i = 0; i < imagePaths.Length; i++)
            {
                allCards.Add(new Card(i * 2, imagePaths[i]));
                allCards.Add(new Card(i * 2 + 1, imagePaths[i]));
            }

            
            Random rng = new Random();
            Cards = allCards.OrderBy(x => rng.Next()).ToList();

            FirstFlippedCard = null;
        }


        public void CardClicked(int cardId)
        {
            var clickedCard = Cards.FirstOrDefault(c => c.Id == cardId);

            if (clickedCard == null || clickedCard.IsMatched || clickedCard.IsFlipped)
                return;

            clickedCard.IsFlipped = true;

            if (FirstFlippedCard == null)
            {
                FirstFlippedCard = clickedCard;
            }
            else
            {
                if (FirstFlippedCard.ImagePath == clickedCard.ImagePath)
                {
                    FirstFlippedCard.IsMatched = true;
                    clickedCard.IsMatched = true;
                }

                FirstFlippedCard = null;

                
                CardsUpdated?.Invoke(this, EventArgs.Empty);

                CheckForWin();
            }

            CardsUpdated?.Invoke(this, EventArgs.Empty); 

        }

        public void FlipBackUnmatchedCards()
        {
            foreach (var card in Cards)
            {
                if (!card.IsMatched)
                    card.IsFlipped = false;
            }

            CardsUpdated?.Invoke(this, EventArgs.Empty); 
        }

        private void CheckForWin()
        {
            if (Cards.All(c => c.IsMatched))
            {
                GameWon?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
