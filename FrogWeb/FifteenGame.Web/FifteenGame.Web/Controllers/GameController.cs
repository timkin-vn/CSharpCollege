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

        public ActionResult Index()
        {
            var field = GetGameField();
            _service.Initialize(field);
            SaveGameField(field);

            return View(ToViewModel(field));
        }

        public ActionResult RemoveAlgae(int row, int column)
        {
            var field = GetGameField();
            _service.RemoveAlgae(field, row, column);
            SaveGameField(field);

            return View("Index", ToViewModel(field));
        }

        public ActionResult SelectLilyPad(int row, int column)
        {
            var field = GetGameField();
            _service.SelectLilyPad(field, row, column);
            SaveGameField(field);

            return View("Index", ToViewModel(field));
        }

        public ActionResult MoveLilyPad(int targetRow, int targetColumn)
        {
            var field = GetGameField();
            _service.MoveLilyPad(field, targetRow, targetColumn);
            SaveGameField(field);

            return View("Index", ToViewModel(field));
        }

        public ActionResult Restart()
        {
            var field = GetGameField();
            _service.Initialize(field);
            SaveGameField(field);

            return View("Index", ToViewModel(field));
        }

        private GameField GetGameField()
        {
            if (Session.IsNewSession)
            {
                Session["GameField"] = new GameField();
            }

            return (GameField)Session["GameField"];
        }

        private void SaveGameField(GameField field)
        {
            Session["GameField"] = field;
        }

        private GameViewModel ToViewModel(GameField field)
        {
            var result = new GameViewModel
            {
                IsGameOver = field.IsGameOver,
                HasSelectedLilyPad = field.SelectedLilyPadRow.HasValue,
                MovesCount = field.MovesCount
            };

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Type = field[row, column],
                        Row = row,
                        Column = column
                    };
                }
            }

            if (field.IsGameOver)
            {
                result.GameStatus = field.IsWin ?
                    $"🎉 Победа! Лягушка дома! 🎉 Потрачено ходов: {field.MovesCount}" :
                    "Игра завершена";
            }
            else
            {
                string mode = field.SelectedLilyPadRow.HasValue ?
                    "Выберите куда переместить кувшинку (кликните на воду)" :
                    "Убирайте водоросли (🌱) или выбирайте кувшинки (🌿)";
                result.GameStatus = $"Ходов: {field.MovesCount} - {mode}";
            }

            return result;
        }
    }
}