using MemoryGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc; 

namespace MemoryGames.Controllers 
{
    public class GameController : Controller 
    {
        
        private const string GameModelSessionKey = "GameModel";

       
        private GameModel GetGameModel()
        {
            GameModel gameModel = Session[GameModelSessionKey] as GameModel;
            if (gameModel == null)
            {
                gameModel = new GameModel(3, 2);
                Session[GameModelSessionKey] = gameModel;
            }
            return gameModel;
        }

        public ActionResult Index()
        {
            GameModel gameModel = GetGameModel();
            return View(gameModel.Cards);
        }

        [HttpPost]
        public JsonResult FlipCard(int id)
        {

            GameModel gameModel = GetGameModel();

            gameModel.CardClicked(id);


            if (gameModel.FirstFlippedCard == null)
            {
                
                System.Threading.Thread.Sleep(1000); 
                gameModel.FlipBackUnmatchedCards();
            }


            return Json(gameModel.Cards.Select(c => new { c.Id, c.ImagePath, c.IsFlipped, c.IsMatched }));
        }
    }
}
