using System.Web.Mvc;
using FifteenGame.Core.BusinessLogic.Services;

namespace FifteenGame.Web.Controllers
{
    public class HomeController : Controller
    {
        private GameService GetGame()
        {
            var game = Session["Game"] as GameService;
            if (game == null)
            {
                game = new GameService();
                Session["Game"] = game;
            }

            return game;
        }

        public ActionResult Index()
        {
            return View(GetGame());
        }

        [HttpPost]
        public ActionResult Move(int dRow, int dColumn)
        {
            var game = GetGame();
            game.TryMove(dRow, dColumn);
            return PartialView("_GameGrid", game);
        }

        [HttpPost]
        public ActionResult Restart()
        {
            var game = GetGame();
            game.NewGame();
            return PartialView("_GameGrid", game);
        }
    }
}
