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
                _service.MakeMove(model, direction);

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
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Num = model[row, column],
                        IsEmpty = model[row, column] == GameModel.FreeCellValue,
                        Direction = MoveDirection.None,
                    };
                }
            }

            if (model.FreeCellColumn > 0)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn - 1].Direction = MoveDirection.Right;
            }

            if (model.FreeCellColumn < GameModel.ColumnCount - 1)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn + 1].Direction = MoveDirection.Left;
            }

            if (model.FreeCellRow > 0)
            {
                result.Cells[model.FreeCellRow - 1, model.FreeCellColumn].Direction = MoveDirection.Down;
            }

            if (model.FreeCellRow < GameModel.RowCount - 1)
            {
                result.Cells[model.FreeCellRow + 1, model.FreeCellColumn].Direction = MoveDirection.Up;
            }

            return result;
        }
    }
}