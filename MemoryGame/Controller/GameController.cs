using MemoryGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Controller
{
    public class GameController
    {
        public GameModel GameModel { get; }

        public GameController(GameModel gameModel)
        {
            GameModel = gameModel;
        }

        public void CardClicked(int cardId)
        {
            GameModel.CardClicked(cardId);
        }

        public void FlipBackUnmatchedCards()
        {
            GameModel.FlipBackUnmatchedCards();
        }
    }
}
