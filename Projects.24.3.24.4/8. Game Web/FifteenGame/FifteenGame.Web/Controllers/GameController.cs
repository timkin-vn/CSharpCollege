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

            if (model.Cells.Count == 0)
            {
                _service.Initialize(model);
                SaveGameModel(model);
            }

            return View(ToViewModel(model));
        }

        public ActionResult NewGame(int rows = 2, int columns = 5)
        {
            var model = new GameModel();

            model.RowCount = rows;
            model.ColumnCount = columns;

            if (rows == 2 && columns == 5)
            {
                model.DifficultyName = "Легкая";
            }
            else if (rows == 4 && columns == 4)
            {
                model.DifficultyName = "Средняя";
            }
            else if (rows == 4 && columns == 5)
            {
                model.DifficultyName = "Сложная";
            }

            _service.Initialize(model);

            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        public ActionResult Pause()
        {
            var model = GetGameModel();

            model.IsPaused = !model.IsPaused;

            if (model.IsPaused)
            {
                model.PauseTime = DateTime.Now;
            }
            else
            {
                if (model.PauseTime.HasValue)
                {
                    model.StartTime = model.StartTime.Add(
                        DateTime.Now - model.PauseTime.Value
                    );

                    model.PauseTime = null;
                }
            }

            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        public ActionResult StartGame()
        {
            var model = GetGameModel();

            foreach (var cell in model.Cells)
            {
                cell.IsOpened = false;
            }

            model.IsPreview = false;

            SaveGameModel(model);

            return RedirectToAction("Index");
        }

        public ActionResult OpenCell(int index)
        {
            var model = GetGameModel();

            if (model.IsPaused)
            {
                return View("Index", ToViewModel(model));
            }

            if (model.IsCheckingPair)
            {
                return View("Index", ToViewModel(model));
            }

            _service.OpenCell(model, index);

            if (model.SecondOpenedCell != -1)
            {
                model.IsCheckingPair = true;
            }

            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        public ActionResult CheckPair()
        {
            var model = GetGameModel();

            _service.CheckOpenedCells(model);

            model.IsCheckingPair = false;

            SaveGameModel(model);

            if (_service.IsGameOver(model))
            {
                model.IsGameOver = true;

                if (model.MovesCount < model.BestMoves || (model.MovesCount == model.BestMoves && model.Seconds < model.BestSeconds))
                {
                    model.BestMoves = model.MovesCount;
                    model.BestSeconds = model.Seconds;
                    model.IsNewRecord = true;
                }
            }

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
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
            if (!model.IsPaused)
            {
                model.Seconds = (int)(DateTime.Now - model.StartTime).TotalSeconds;
            }

            var result = new GameViewModel();

            result.IsPreview = model.IsPreview;
            result.IsPaused = model.IsPaused;
            result.IsGameOver = model.IsGameOver;
            result.IsNewRecord = model.IsNewRecord;
            result.RowCount = model.RowCount;
            result.ColumnCount = model.ColumnCount;
            result.DifficultyName = model.DifficultyName;

            for (int i = 0; i < model.Cells.Count; i++)
            {
                result.Cells.Add(new CellViewModel
                {
                    Index = i,
                    Row = i / model.ColumnCount,
                    Column = i % model.ColumnCount,

                    Color = model.Cells[i].Color,
                    IsOpened = model.Cells[i].IsOpened,
                    IsFound = model.Cells[i].IsFound
                });
            }

            result.MovesCount = model.MovesCount;
            result.Seconds = model.Seconds;
            result.BestMoves = model.BestMoves;
            result.BestSeconds = model.BestSeconds;


            return result;
        }
    }
}