using Microsoft.AspNetCore.Mvc;
using MinesweeperWeb.Models;
using MinesweeperWeb.Services;
using System.Collections.Generic;

namespace MinesweeperWeb.Controllers
{
    public class GameController : Controller
    {
        private static GameService _gameService;

        public GameController()
        {
            if (_gameService == null)
            {
                _gameService = new GameService(10, 10, 10); // 10x10 поле, 10 мин
            }
        }

        public IActionResult Index()
        {
            var model = BuildViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult OpenCell(int row, int col)
        {
            _gameService.OpenCell(row, col, () => { });
            var model = BuildViewModel();
            return Json(model);
        }

        [HttpPost]
        public IActionResult FlagCell(int row, int col)
        {
            _gameService.FlagCell(row, col);
            var model = BuildViewModel();
            return Json(model);
        }

        [HttpPost]
        public IActionResult RestartGame()
        {
            _gameService.StartNewGame();
            var model = BuildViewModel();
            return Json(model);
        }

        private GameViewModel BuildViewModel()
        {
            var gameField = _gameService.GameModel.Field;
            var field = new List<List<CellViewModel>>();

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                var row = new List<CellViewModel>();
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    var cell = new CellViewModel(i, j)
                    {
                        IsOpen = gameField[i, j].IsOpen,
                        IsFlagged = gameField[i, j].IsFlagged,
                        IsMine = gameField[i, j].IsMine,
                        NeighborMines = gameField[i, j].NeighborMines
                    };
                    row.Add(cell);
                }
                field.Add(row);
            }

            return new GameViewModel
            {
                Field = field,
                IsGameOver = _gameService.IsGameOver,
                IsWin = _gameService.IsWin
            };
        }
    }
}