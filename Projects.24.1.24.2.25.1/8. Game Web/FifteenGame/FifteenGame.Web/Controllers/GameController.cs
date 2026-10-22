using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService = new GameService();
        private const string SessionKey = "Current2048Game";

        public ActionResult Index()
        {
            var game = GetOrCreateGame();
            _gameService.ResetState(game);
            SaveGame(game);

            return View(CreateViewModel(game));
        }

        public ActionResult MakeMove(string directionText)
        {
            var game = GetOrCreateGame();

            
            if (Enum.TryParse(directionText, true, out MoveDirection direction))
            {
                _gameService.MakeMove(game, direction);

                
                if (!game.IsWin)
                {
                    CheckVictoryCondition(game);
                }

                SaveGame(game);
            }

            return View("Index", CreateViewModel(game));
        }

        public ActionResult NewGame()
        {
            var newGame = new GameModel();
            _gameService.ResetState(newGame);
            SaveGame(newGame);

            return RedirectToAction("Index");
        }

        

        private GameModel GetOrCreateGame()
        {
            return Session[SessionKey] as GameModel ?? new GameModel();
        }

        private void SaveGame(GameModel game)
        {
            Session[SessionKey] = game;
        }

        private void CheckVictoryCondition(GameModel game)
        {
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    if (game[r, c] == 2048)
                    {
                        game.IsWin = true;
                        return;
                    }
                }
            }
        }

        private GameViewModel CreateViewModel(GameModel domainModel)
        {
            return new GameViewModel
            {
                Score = domainModel.Score,
                IsWin = domainModel.IsWin,
                IsGameOver = _gameService.CheckIfLost(domainModel),
                Matrix = (int[,])domainModel.Cells.Clone()
            };
        }
    }
}