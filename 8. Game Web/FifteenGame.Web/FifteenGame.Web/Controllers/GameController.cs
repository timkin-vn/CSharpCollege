using Minesweeper.Web.Models;
using Minesweeper.Business.Services;
using Minesweeper.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper.Web.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            var model = GetModel();
            var debugMode = Session["MinesweeperDebugMode"] as bool? ?? false;

            return View(new MinesweeperViewModel
            {
                GameField = model,
                DebugMode = debugMode
            });
        }

        [HttpPost]
        public ActionResult RevealCell(int row, int col)
        {
            var model = GetModel();
            model.RevealCell(row, col);
            SaveModel(model);

            if (model.GameOver)
            {
                TempData["Message"] = "Игра окончена! Вы наступили на мину!";
            }
            else if (model.GameWon)
            {
                TempData["Message"] = "Поздравляем! Вы выиграли!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleFlag(int row, int col)
        {
            var model = GetModel();
            model.ToggleFlag(row, col);
            SaveModel(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult NewGame(int size = 10, int mineCount = 15)
        {
            var model = new MinesweeperField(size, mineCount);
            SaveModel(model);
            Session["MinesweeperDebugMode"] = false;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleDebug()
        {
            var currentDebugMode = Session["MinesweeperDebugMode"] as bool? ?? false;
            Session["MinesweeperDebugMode"] = !currentDebugMode;
            return RedirectToAction("Index");
        }

        private MinesweeperField GetModel()
        {
            if (Session["MinesweeperField"] == null)
            {
                Session["MinesweeperField"] = new MinesweeperField();
            }

            return (MinesweeperField)Session["MinesweeperField"];
        }

        private void SaveModel(MinesweeperField model)
        {
            Session["MinesweeperField"] = model;
        }
    }
}