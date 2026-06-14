using Microsoft.AspNetCore.Mvc;
using Minesweeper.Business.Models;
using MinesweeperWeb.Models;
using MinesweeperWeb.Services;
using System.Text.Json;

namespace MinesweeperWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly MinesweeperService _service;

        public GameController()
        {
            _service = new MinesweeperService();
        }

        // GET: /Game
        public IActionResult Index()
        {
            var model = GetOrCreateGameModel();
            var viewModel = _service.ToViewModel(model);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Click(int row, int col)
        {
            var model = GetGameModel();
            if (model == null || model.State != GameState.Playing)
            {
                return RedirectToAction("Index");
            }

            _service.HandleClick(model, row, col);
            SaveGameModel(model);

            var viewModel = _service.ToViewModel(model);
            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult Flag(int row, int col)
        {
            var model = GetGameModel();
            if (model == null || model.State != GameState.Playing)
            {
                return RedirectToAction("Index");
            }

            _service.HandleFlag(model, row, col);
            SaveGameModel(model);

            var viewModel = _service.ToViewModel(model);
            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult NewGame()
        {
            var model = _service.CreateNewGame();
            SaveGameModel(model);

            var viewModel = _service.ToViewModel(model);
            return View("Index", viewModel);
        }

        private GameModel GetOrCreateGameModel()
        {
            var model = HttpContext.Session.GetObject<GameModel>("GameModel");
            if (model == null)
            {
                model = _service.CreateNewGame();
                SaveGameModel(model);
            }
            return model;
        }

        private GameModel GetGameModel()
        {
            return HttpContext.Session.GetObject<GameModel>("GameModel");
        }

        private void SaveGameModel(GameModel model)
        {
            HttpContext.Session.SetObject("GameModel", model);
        }
    }
}