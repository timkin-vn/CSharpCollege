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
            var model = GetModel();
            SaveModel(model);
            return View(GetViewModel(model));
        }

        public ActionResult PressButton(string directionText)
        {
            var model = GetModel();
            if (Enum.TryParse<MoveDirection>(directionText, out var direction))
            {
                if (_service.Move(model, direction))
                {
                    SaveModel(model);
                }

                if (_service.HasWon(model))
                {
                    ViewBag.Message = "Поздравляем! Вы собрали 2048!";
                }
                else if (_service.IsGameOver(model))
                {
                    ViewBag.Message = "Игра окончена! Нет доступных ходов.";
                }
            }
            return View("Index", GetViewModel(model));
        }

        private GameModel GetModel()
        {
            if (Session["GameModel"] == null)
            {
                var model = new GameModel();
                model.Reset();
                Session["GameModel"] = model;
            }
            return (GameModel)Session["GameModel"];
        }

        private void SaveModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel GetViewModel(GameModel model)
        {
            var result = new GameViewModel();
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    result.Cells[row, col] = new CellViewModel
                    {
                        Num = model[row, col]
                    };
                }
            }
            return result;
        }
    }
}
