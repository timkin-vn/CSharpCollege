using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var sessionData = LoadState();
            _service.ResetState(sessionData);
            CommitState(sessionData);
            return View(MapToView(sessionData));
        }

        public ActionResult MakeMove(string directionText)
        {
            var sessionData = LoadState();

            if (!Enum.TryParse(directionText, out MoveDirection parsedDir))
            {
                return View("Index", MapToView(sessionData));
            }

            _service.MakeMove(sessionData, parsedDir);

            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    if (sessionData[r, c] == 2048 && !sessionData.IsWin)
                    {
                        sessionData.IsWin = true;
                    }
                }
            }

            CommitState(sessionData);
            return View("Index", MapToView(sessionData));
        }

        public ActionResult NewGame()
        {
            var freshGame = new GameModel();
            _service.ResetState(freshGame);
            CommitState(freshGame);
            return RedirectToAction("Index");
        }

        private GameModel LoadState()
        {
            if (Session["GameModel"] == null)
            {
                Session["GameModel"] = new GameModel();
            }
            return (GameModel)Session["GameModel"];
        }

        private void CommitState(GameModel state) => Session["GameModel"] = state;

        private GameViewModel MapToView(GameModel domain)
        {
            var viewData = new GameViewModel
            {
                Score = domain.Score,
                IsWin = domain.IsWin,
                IsGameOver = _service.CheckIfLost(domain)
            };

            int len = GameModel.Size;
            for (int i = 0; i < len * len; i++)
            {
                int row = i / len;
                int col = i % len;
                viewData.Matrix[row, col] = domain[row, col];
            }

            return viewData;
        }
    }
}