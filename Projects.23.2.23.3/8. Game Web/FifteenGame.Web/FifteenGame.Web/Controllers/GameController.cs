using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

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

        private GameField GetModel()
        {
            if (Session.IsNewSession)
            {
                Session["GameField"] = new GameField();
            }

            return (GameField)Session["GameField"];
        }

        private void SaveModel(GameField model)
        {
            Session["GameField"] = model;
        }

        private GameViewModel GetViewModel(GameField model)
        {
            var result = new GameViewModel();
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int col = 0; col < GameField.ColumnCount; col++)
                {
                    var cell = new CellViewModel
                    {
                        Value = model[row, col],
                        IsEmpty = model[row, col] == GameField.FreeCellValue,
                        Direction = MoveDirection.None,
                    };

                    result.Cells[row, col] = cell;
                }
            }

            if (model.FreeCellColumn > 0)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn - 1].Direction = MoveDirection.Right;
            }

            if (model.FreeCellColumn < GameField.ColumnCount - 1)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn + 1].Direction = MoveDirection.Left;
            }

            if (model.FreeCellRow > 0)
            {
                result.Cells[model.FreeCellRow - 1, model.FreeCellColumn].Direction = MoveDirection.Down;
            }

            if (model.FreeCellRow < GameField.RowCount - 1)
            {
                result.Cells[model.FreeCellRow + 1, model.FreeCellColumn].Direction = MoveDirection.Up;
            }

            return result;
        }
    }
}