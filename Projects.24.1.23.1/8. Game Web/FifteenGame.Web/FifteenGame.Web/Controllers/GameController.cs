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
        private const string GameSessionKey = "GameModel";
        private const string SelectedCellKey = "SelectedCell";

        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetOrCreateGame();
            return View(ToViewModel(model));
        }

        public ActionResult Select(int row, int col)
        {
            var model = GetOrCreateGame();

            var selected = Session[SelectedCellKey] as (int r, int c)?;

            if (selected == null)
            {
                Session[SelectedCellKey] = (row, col);
            }
            else
            {
                var (r1, c1) = selected.Value;

                if (_service.Swap(model, r1, c1, row, col))
                {
                    while (true)
                    {
                        var matches = _service.CheckMatches(model);
                        if (!matches.Any())
                            break;

                        _service.AddMatches(model, matches.Count);
                        _service.RemoveMatches(model, matches);
                        _service.ProcessMatches(model);
                    }
                }

                Session.Remove(SelectedCellKey);
            }

            SaveGame(model);
            return RedirectToAction("Index");
        }

        private GameModel GetOrCreateGame()
        {
            if (Session[GameSessionKey] == null)
            {
                var model = new GameModel();
                _service.Initialize(model);
                Session[GameSessionKey] = model;
            }

            return (GameModel)Session[GameSessionKey];
        }

        public ActionResult NewGame()
        {
            var model = new GameModel();
            _service.Initialize(model);
            Session[GameSessionKey] = model;

            Session.Remove(SelectedCellKey); // сброс выбора

            return RedirectToAction("Index");
        }

        private void SaveGame(GameModel model)
        {
            Session[GameSessionKey] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var vm = new GameViewModel
            {
                RowCount = GameModel.RowCount,
                ColumnCount = GameModel.ColumnCount,
                MatchesCount = model.MatchesCount,
                IsFinished = model.IsFinished,
                Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount]
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    vm.Cells[r, c] = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        GemType = model[r, c]
                    };
                }
            }

            return vm;
        }
    }
}