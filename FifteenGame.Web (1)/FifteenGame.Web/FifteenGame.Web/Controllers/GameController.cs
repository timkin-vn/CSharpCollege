using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private static TicTacToeGame _currentGame;
        private static TicTacToeService _gameService = new TicTacToeService();

        public ActionResult Index()
        {
            if (_currentGame == null)
            {
                _currentGame = _gameService.InitializeGame();
            }

            return View(MapToViewModel(_currentGame));
        }

        [HttpPost]
        public ActionResult NewGame()
        {
            _currentGame = _gameService.InitializeGame();
            return Json(MapToViewModel(_currentGame));
        }

        [HttpPost]
        public ActionResult MakeMove(int row, int column)
        {
            _gameService.MakeMove(_currentGame, row, column);
            return Json(MapToViewModel(_currentGame));
        }

        private TicTacToeViewModel MapToViewModel(TicTacToeGame game)
        {
            var viewModel = new TicTacToeViewModel
            {
                CurrentPlayer = game.CurrentPlayer,
                GameState = game.GameState,
                Winner = game.Winner,
            };

            for (int r = 0; r < TicTacToeGame.RowCount; r++)
            {
                for (int c = 0; c < TicTacToeGame.ColumnCount; c++)
                {
                    viewModel.Cells.Add(new TicTacToeCellViewModel
                    {
                        Row = r,
                        Column = c,
                        Player = game[r, c]
                    });
                }
            }

            if (game.GameState == GameState.Won)
            {
                viewModel.GameStatus = $"Игрок {game.Winner} выиграл!";
            }
            else if (game.GameState == GameState.Draw)
            {
                viewModel.GameStatus = "Ничья!";
            }
            else
            {
                viewModel.GameStatus = $"Ход игрока {game.CurrentPlayer}";
            }

            return viewModel;
        }
    }
}
