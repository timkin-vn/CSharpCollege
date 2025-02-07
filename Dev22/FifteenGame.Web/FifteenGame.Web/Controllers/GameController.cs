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

        // GET: Game
        public ActionResult Index()
        {
            var model = GetModel();
            _service.Shuffle(model);
            SaveModel(model);

            return View(GetViewModel(model));
        }

        public ActionResult PressButton(string directionText)
        {
            var model = GetModel();

            if (Enum.TryParse<MoveDirection>(directionText, out var direction))
            {
                _service.MakeMove(model, direction);
                SaveModel(model);

                if (_service.IsGameOver(model))
                {
                    ViewBag.IsGameOver = true;
                }
            }

            return View("Index", GetViewModel(model));
        }

        private GameModel GetModel()
        {
            if (Session.IsNewSession)
            {
                Session["GameModel"] = new GameModel();
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