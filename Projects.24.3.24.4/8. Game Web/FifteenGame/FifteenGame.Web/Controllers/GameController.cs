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
            // Если игра только началась инициализируем карту
            if (model.TurnsPlayed == 0 && model.Money == 0)
            {
                _service.Initialize(model);
            }

            return View(ToViewModel(model));
        }

        // Метод обработки клика по клетке
        public ActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();

            if (_service.MakeMove(model, row, column))
            {
                var status = _service.CheckGameStatus(model);
                if (status == GameResult.GameOver)
                {
                    Session["StatusMessage"] = "Банкротство! Вы ушли в минус. Шаурмичные закрыты.";
                }
                else if (status == GameResult.Win)
                {
                    Session["StatusMessage"] = "Поздравляем! Ваш бизнес успешно накормил город за 10 ходов!";
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Reset()
        {
            Session["GameModel"] = null;
            Session["StatusMessage"] = null;
            return RedirectToAction("Index");
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
            {
                Session["GameModel"] = new GameModel();
            }
            return (GameModel)Session["GameModel"];
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel
            {
                MoneyText = $"Баланс: {model.Money} руб.",
                TurnsText = $"Ход: {model.TurnsPlayed} / {GameModel.TargetTurns}",
                GameStatusMessage = Session["StatusMessage"] as string
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    bool covered = _service.IsCellCovered(model, row, column);
                    int veggieNeighbors = _service.GetVeggieNeighborsCount(model, row, column);

                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        PeopleCount = model.GetPeopleCount(row, column),
                        HasShop = model.GetHasShop(row, column),
                        IsCovered = covered,
                        IsVeggie = model.GetIsVeggie(row, column),
                        IsRevealed = model.GetIsRevealed(row, column),
                        VeggieNeighborsCount = veggieNeighbors
                    };
                }
            }

            return result;
        }
    }
}