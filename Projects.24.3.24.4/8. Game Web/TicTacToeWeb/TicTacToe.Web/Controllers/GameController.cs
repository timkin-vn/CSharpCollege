using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicTacToe.Business.Models;
using TicTacToe.Business.Services;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService = new GameService();

        // Специальный плоский класс для обхода ограничений JSON-сериализации двумерных массивов
        public class GameStateDto
        {
            public List<CellValue> Cells { get; set; } = new List<CellValue>();
            public CellValue CurrentPlayer { get; set; }
            public GameResult Result { get; set; }
            public int MovesCount { get; set; }
        }

        private GameModel GetGameModel()
        {
            var dto = HttpContext.Session.Get<GameStateDto>("GameState");
            var model = new GameModel();

            if (dto == null)
            {
                _gameService.Initialize(model);
                SaveGameModel(model);
            }
            else
            {
                model.CurrentPlayer = dto.CurrentPlayer;
                model.Result = dto.Result;
                model.MovesCount = dto.MovesCount;

                int index = 0;
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        model[r, c] = dto.Cells[index++];
                    }
                }
            }
            return model;
        }

        private void SaveGameModel(GameModel model)
        {
            var dto = new GameStateDto
            {
                CurrentPlayer = model.CurrentPlayer,
                Result = model.Result,
                MovesCount = model.MovesCount,
                Cells = new List<CellValue>()
            };

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    dto.Cells.Add(model[r, c]);
                }
            }

            HttpContext.Session.Set("GameState", dto);
        }

        public IActionResult Index()
        {
            return View(CreateViewModel(GetGameModel()));
        }

        [HttpPost]
        public IActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();
            if (_gameService.MakeMove(model, row, column))
            {
                // Если игра продолжается, бот делает свой ответный ход
                if (!_gameService.IsGameOver(model) && model.CurrentPlayer == CellValue.O)
                {
                    _gameService.MakeComputerMove(model);
                }
                SaveGameModel(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            var model = new GameModel();
            _gameService.Initialize(model);
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        private GameViewModel CreateViewModel(GameModel model)
        {
            var viewModel = new GameViewModel
            {
                Cells = new CellValue[3, 3],
                CurrentPlayer = model.CurrentPlayer,
                Result = model.Result,
                IsGameOver = _gameService.IsGameOver(model)
            };

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    viewModel.Cells[r, c] = model[r, c];
                }
            }

            if (viewModel.IsGameOver)
            {
                viewModel.Message = model.Result == GameResult.Draw
                    ? "Ничья!"
                    : $"Победитель: {(model.Result == GameResult.XWins ? "X" : "O")}";
            }
            else
            {
                viewModel.Message = $"Ход игрока: {model.CurrentPlayer}";
            }

            return viewModel;
        }
    }
}