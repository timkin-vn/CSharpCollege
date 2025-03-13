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
            _service.Initialize(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult SelectLetter(int row, int column)
        {
            var model = GetGameModel();
            _service.SelectLetter(model, row, column);

            if (_service.IsGameOver(model))
            {
                ViewBag.IsGameOver = true;
            }

            SaveGameModel(model);

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
            var result = new GameViewModel
            {
                TargetWord = _service.GetTargetWord()
            };
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Letter = model[row, column],
                        IsSelected = model.SelectedCells.Contains((row, column))
                    };
                }
            }

            return result;
        }
    }
}