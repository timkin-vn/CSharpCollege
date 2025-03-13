using MIneSweepper.Bisiness.Model;
using MIneSweepper.Bisiness.Services;
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
        private MineSevicer _service = new MineSevicer();

        // GET: Game
        public ActionResult Index()
        {
            var model = GetModel();
            _service.Iniziallize(model,10);
            SaveModel(model);

            return View(GetViewModel(model));
        }

        public ActionResult PressButton(int row, int column)
        {

            var model = GetModel();

            _service.RevealCell(row, column, model);
            SaveModel(model);

            if (_service.IsGameOver(model))
            {
                ViewBag.IsGameOver = true;
            }
            else
            {
                ViewBag.IsGameOver = false; 
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


            result.Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount];

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    var cell = model[row, column];
                    result.Cells[row, column] = new CellViewModel
                    {
                        IsRevealed = cell.IsRevealed,
                        IsMine = cell.IsMine,
                        Num = cell.NeightborMines,
                    };
                }
            }

            return result;
        }
    }
}