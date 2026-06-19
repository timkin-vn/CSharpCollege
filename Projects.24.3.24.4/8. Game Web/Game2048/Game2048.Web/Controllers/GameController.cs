using System;
using System.Web.Mvc;
using Game2048.Business;
using Game2048.Web.Models;

namespace Game2048.Web.Controllers
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
        public ActionResult MakeMove(string directionText)
        {
            var game = Current;
            if (game != null)
            {
                MoveDirection direction;
                if (Enum.TryParse(directionText, true, out direction))
                {
                    if (!_service.IsWon(game) && !_service.IsGameOver(game))
                    {
                        _service.MakeMove(game, direction);
                        Current = game;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        private GameViewModel BuildViewModel(GameModel game)
        {
            string status = "Игра идёт";
            if (_service.IsWon(game)) status = "Победа! Собрана плитка 2048";
            else if (_service.IsGameOver(game)) status = "Игра окончена — ходов больше нет";

            return new GameViewModel
            {
                Field = game.Field,
                Score = game.Score,
                MoveCount = game.MoveCount,
                Status = status
            };
        }
    }
}
