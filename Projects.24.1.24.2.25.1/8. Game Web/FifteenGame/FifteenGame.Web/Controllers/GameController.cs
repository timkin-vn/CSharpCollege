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
        private readonly GameService _service = new GameService();

        // GET: Game
        public ActionResult Index()
        {
            var model = GetGameModel();
            _service.Shuffle(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();
            _service.MakeMove(model, row, column);
            SaveGameModel(model);

            if (_service.IsGameOver(model))
            {
                ViewBag.IsGameOver = true;
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
                        Value = model[row, column],
                    };
                }
            }

            return result;
        }
    }
}
