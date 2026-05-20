using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetModel();
            if (Session.IsNewSession)
            {
                _service.Initialize(model);
                SaveModel(model);
            }

            return View(GetViewModel(model));
        }

        public ActionResult SelectGem(int row, int column)
        {
            var model = GetModel();
            bool moveMade = _service.SelectGem(model, row, column);
            SaveModel(model);

            var viewModel = GetViewModel(model);

            if (model.GameState != GameState.Playing)
            {
                viewModel.GameOverMessage = model.GameState == GameState.Won
                    ? $"Поздравляем с победой! Набрано {model.Score} очков!"
                    : $"Игра окончена. Набрано {model.Score} очков.";
            }

            return View("Index", viewModel);
        }



        public ActionResult NewGame()
        {
            Session.Remove("GameField");

            var newModel = new GameField();
            _service.Initialize(newModel);
            SaveModel(newModel);

            return RedirectToAction("Index");
        }

        private GameField GetModel()
        {
            if (Session["GameField"] == null)
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
            var viewModel = new GameViewModel
            {
                Score = model.Score,
                MovesLeft = model.MovesLeft,
                GameState = model.GameState,
                Cells = new CellViewModel[GameField.RowCount, GameField.ColumnCount]
            };

            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    viewModel.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        GemType = model[row, column],
                        IsSelected = (row == model.SelectedRow && column == model.SelectedColumn),
                        IsMatched = model.Matched[row, column]
                    };
                }
            }

            return viewModel;
        }
    }
}