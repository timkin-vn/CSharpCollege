using System.Web.Mvc;
using FifteenGame.Business.Services;

namespace FifteenGame.Web.Controllers
{
    public class HomeController : Controller
    {
        private GameManager GetOrCreateGameManager()
        {
            if (Session["GameManager"] == null)
            {
                Session["GameManager"] = new GameManager();
            }
            return (GameManager)Session["GameManager"];
        }

        public ActionResult Index()
        {
            var manager = GetOrCreateGameManager();
            return View(manager);
        }

        [HttpPost]
        public ActionResult Move(int deltaRow, int deltaCol)
        {
            var manager = GetOrCreateGameManager();
            manager.TryMove(deltaRow, deltaCol);
            return PartialView("_GameGrid", manager);
        }

        [HttpPost]
        public ActionResult NextLevel()
        {
            var manager = GetOrCreateGameManager();
            manager.NextLevel();
            return PartialView("_GameGrid", manager);
        }

        [HttpPost]
        public ActionResult RestartGame()
        {
            var manager = GetOrCreateGameManager();
            manager.RestartGame();
            return PartialView("_GameGrid", manager);
        }
    }
}
