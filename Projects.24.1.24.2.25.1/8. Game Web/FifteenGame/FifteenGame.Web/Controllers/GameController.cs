
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

        private const string GameModelKey = "GameModel";
        private const string MoveCountKey = "MoveCount";

        public ActionResult Index()
        {
            var model = GetGameModel();

            return View(ToViewModel(model));
        }

        public ActionResult MakeMove(string directionText)
        {
            var model = GetGameModel();

            if (Enum.TryParse(directionText, out MoveDirection direction)
                && direction != MoveDirection.None
                && !_service.IsGameOver(model))
            {
                if (_service.MakeMove(model, direction))
                {
                    int moveCount = GetMoveCount() + 1;
                    Session[MoveCountKey] = moveCount;
                }

                SaveGameModel(model);

                if (_service.IsGameOver(model))
                {
                    ViewBag.IsGameOver = true;
                }
            }

            return View("Index", ToViewModel(model));
        }

        public ActionResult Restart()
        {
            var model = new GameModel();

            _service.Shuffle(model);

            SaveGameModel(model);
            Session[MoveCountKey] = 0;

            return RedirectToAction("Index");
        }

        private GameModel GetGameModel()
        {
            var model = Session[GameModelKey] as GameModel;

            if (model == null)
            {
                model = new GameModel();
                _service.Shuffle(model);

                SaveGameModel(model);
                Session[MoveCountKey] = 0;
            }

            return model;
        }

        private void SaveGameModel(GameModel model)
        {
            Session[GameModelKey] = model;
        }

        private int GetMoveCount()
        {
            return Session[MoveCountKey] == null
                ? 0
                : (int)Session[MoveCountKey];
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel
            {
                MoveCount = GetMoveCount()
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Value = model[row, column],
                        Direction = MoveDirection.None,
                        IsEmpty = model[row, column] == GameModel.FreeCellValue
                    };
                }
            }

            if (model.FreeCellColumn > 0)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn - 1]
                    .Direction = MoveDirection.Right;
            }

            if (model.FreeCellColumn < GameModel.ColumnCount - 1)
            {
                result.Cells[model.FreeCellRow, model.FreeCellColumn + 1]
                    .Direction = MoveDirection.Left;
            }

            if (model.FreeCellRow > 0)
            {
                result.Cells[model.FreeCellRow - 1, model.FreeCellColumn]
                    .Direction = MoveDirection.Down;
            }

            if (model.FreeCellRow < GameModel.RowCount - 1)
            {
                result.Cells[model.FreeCellRow + 1, model.FreeCellColumn]
                    .Direction = MoveDirection.Up;
            }

            return result;
        }
    }
}
