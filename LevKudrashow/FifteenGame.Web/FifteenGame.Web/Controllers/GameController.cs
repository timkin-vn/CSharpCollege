using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetGameModel();
            _service.Shuffle(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult PressButton(string directionText)
        {
            var model = GetGameModel();
            if (Enum.TryParse<MoveDirection>(directionText, out var direction))
            {
                _service.MakeMove(model, direction, false);

                if (_service.IsGameOver(model))
                {
                    ViewBag.IsGameOver = true;
                }

                SaveGameModel(model);
            }

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            if (Session.IsNewSession)
            {
                Session["GameModel"] = new GameModel();
            }

            return (GameModel)Session["GameModel"];
        }

        private void SaveGameModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel();
            result.StepsLeft = model.StepsLeft;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Num = model[row, column],
                        IsPlayer = row == model.PlayerRow && column == model.PlayerColumn,
                        Direction = MoveDirection.None,
                    };
                }
            }

            if (model.PlayerColumn > 0)
            {
                result.Cells[model.PlayerRow, model.PlayerColumn - 1].Direction = MoveDirection.Right;
            }

            if (model.PlayerColumn < GameModel.ColumnCount - 1)
            {
                result.Cells[model.PlayerRow, model.PlayerColumn + 1].Direction = MoveDirection.Left;
            }

            if (model.PlayerRow > 0)
            {
                result.Cells[model.PlayerRow - 1, model.PlayerColumn].Direction = MoveDirection.Down;
            }

            if (model.PlayerRow < GameModel.RowCount - 1)
            {
                result.Cells[model.PlayerRow + 1, model.PlayerColumn].Direction = MoveDirection.Up;
            }

            return result;
        }
    }
}