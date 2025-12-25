using Game2048.Business.Models;
using Game2048.Business.Services;
using Game2048.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game2048.Web.Controllers
{
    public class GameController : Controller
    {
        private const string GameSessionKey = "GameModel";

        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetOrCreateGame();
            return View(ToViewModel(model));
        }

        public ActionResult Move(string direction)
        {
            var model = GetOrCreateGame();

            if (model.IsGameOver || model.HasWon)
                return RedirectToAction("Index");

            bool moved = false;
            
            switch (direction.ToLower())
            {
                case "up":
                    moved = _service.MoveUp(model);
                    break;
                case "down":
                    moved = _service.MoveDown(model);
                    break;
                case "left":
                    moved = _service.MoveLeft(model);
                    break;
                case "right":
                    moved = _service.MoveRight(model);
                    break;
            }

            SaveGame(model);
            return RedirectToAction("Index");
        }

        private GameModel GetOrCreateGame()
        {
            if (Session[GameSessionKey] == null)
            {
                var model = new GameModel();
                _service.Initialize(model);
                Session[GameSessionKey] = model;
            }

            return (GameModel)Session[GameSessionKey];
        }

        public ActionResult NewGame()
        {
            var model = new GameModel();
            _service.Initialize(model);
            Session[GameSessionKey] = model;

            return RedirectToAction("Index");
        }

        private void SaveGame(GameModel model)
        {
            Session[GameSessionKey] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var vm = new GameViewModel
            {
                RowCount = GameModel.RowCount,
                ColumnCount = GameModel.ColumnCount,
                Score = model.Score,
                IsGameOver = model.IsGameOver,
                HasWon = model.HasWon,
                Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount]
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    vm.Cells[r, c] = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = model[r, c]
                    };
                }
            }

            return vm;
        }
    }
}
