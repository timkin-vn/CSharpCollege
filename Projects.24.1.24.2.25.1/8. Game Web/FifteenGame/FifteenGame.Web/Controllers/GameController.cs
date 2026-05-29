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
            var model = GetGameModel();
            _service.Initialize(model);
            SaveGameModel(model);
            return View(ToViewModel(model));
        }

        public ActionResult MakeMove(string directionText)
        {
            var model = GetGameModel();
            if (Enum.TryParse<MoveDirection>(directionText, out var direction))
            {
                _service.MakeMove(model, direction);
                SaveGameModel(model);

                // Проверка победы (появление 2048)
                for (int i = 0; i < GameModel.Size; i++)
                    for (int j = 0; j < GameModel.Size; j++)
                        if (model[i, j] == 2048 && !model.IsWin)
                        {
                            model.IsWin = true;
                            SaveGameModel(model);
                            ViewBag.Message = "Вы победили! Нажмите «New Game».";
                        }

                if (_service.IsGameOver(model))
                    ViewBag.Message = "Игра окончена! Ходов больше нет.";
            }

            return View("Index", ToViewModel(model));
        }

        public ActionResult NewGame()
        {
            var model = new GameModel();
            _service.Initialize(model);
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
                Session["GameModel"] = new GameModel();
            return (GameModel)Session["GameModel"];
        }

        private void SaveGameModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel
            {
                Score = model.Score,
                IsWin = model.IsWin,
                IsGameOver = _service.IsGameOver(model)
            };

            for (int row = 0; row < GameModel.Size; row++)
                for (int col = 0; col < GameModel.Size; col++)
                    result.Cells[row, col] = model[row, col];

            return result;
        }
    }
}