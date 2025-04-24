using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Game2048.Business.Models;
using Game2048.Business.Services;
using Game2048.WEB.Models;

namespace Game2048.WEB.Controllers
{
    public class GameController : Controller
    {
        private GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = new GameModel();
            _service.Initialize(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult Move(string direction)
        {
            var model = GetGameModel();
            if (Enum.TryParse<MoveDirection>(direction, out var dir))
            {
                _service.MakeMove(model, dir);

                var status = _service.GetGameStatus(model);
                ViewBag.GameStatus = status;

                SaveGameModel(model);
            }

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
            {
                var model = new GameModel();
                _service.Initialize(model);
                Session["GameModel"] = model;
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
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    result.Cells.Add(new CellViewModel
                    {
                        Value = model.Cells[row, col],
                        Row = row,
                        Column = col
                    });
                }
            }
            return result;
        }
        public ActionResult NewGame()
        {
            var model = new GameModel();
            _service.Initialize(model);
            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }
    }
}