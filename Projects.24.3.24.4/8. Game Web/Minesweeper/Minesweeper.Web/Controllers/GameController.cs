using System.Web.Mvc;
using Minesweeper.Business;
using Minesweeper.Web.Models;

namespace Minesweeper.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();
        private const string SessionKey = "game";

        private GameModel Current
        {
            get { return Session[SessionKey] as GameModel; }
            set { Session[SessionKey] = value; }
        }

        public ActionResult Index()
        {
            var game = Current;
            if (game == null)
            {
                game = _service.Initialize();
                Current = game;
            }
            return View(BuildViewModel(game));
        }

        public ActionResult New()
        {
            Current = _service.Initialize();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Cell(int row, int col, string action)
        {
            var game = Current;
            if (game != null)
            {
                if (action == "flag")
                    _service.ToggleFlag(game, row, col);
                else
                    _service.Reveal(game, row, col);
                Current = game;
            }
            return RedirectToAction("Index");
        }

        private GameViewModel BuildViewModel(GameModel game)
        {
            bool won = _service.IsWon(game);
            bool lost = _service.IsGameOver(game);

            string status = "Игра идёт";
            if (won) status = "Победа! Все мины обезврежены";
            else if (lost) status = "Взрыв! Игра окончена";

            return new GameViewModel
            {
                Field = game.Field,
                Status = status,
                MinesRemaining = _service.MinesRemaining(game),
                MoveCount = game.MoveCount,
                Finished = won || lost
            };
        }
    }
}
