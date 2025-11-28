using Match3Game.Business.Models;
using Match3Game.Business.Services;
using Match3GameWeb.Models;
using System.Web.Mvc;

namespace Match3GameWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetGameModel();

            if (model == null)
            {
                model = new GameModel();
                _service.Initialize(model);
                SaveGameModel(model);
            }

            return View(ToViewModel(model));
        }

        public ActionResult Swap(int r1, int c1, int r2, int c2)
        {
            var model = GetGameModel();
            if (model == null) return RedirectToAction("Index");

            bool success = _service.Swap(model, r1, c1, r2, c2);

            if (success)
            {
                var matches = _service.CheckMatches(model);
                _service.RemoveMatches(model, matches);
                _service.ProcessMatches(model);
            }

            SaveGameModel(model);

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            return Session["GameModel"] as GameModel;
        }

        private void SaveGameModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var vm = new GameViewModel
            {
                RowCount = GameModel.RowCount,
                ColumnCount = GameModel.ColumnCount,
                IsGameOver = model.IsGameOver,
                Cells = new CellViewModel[GameModel.RowCount, GameModel.ColumnCount]
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    vm.Cells[r, c] = new CellViewModel
                    {
                        Value = model[r, c]
                    };
                }
            }

            return vm;
        }
    }
}
    