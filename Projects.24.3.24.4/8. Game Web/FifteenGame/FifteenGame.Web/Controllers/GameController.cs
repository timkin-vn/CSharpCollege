using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetGameModel();
            var vm = ToViewModel(model);

            vm.PlayerScore = Session["PlayerScore"] as int? ?? 0;
            vm.ComputerScore = Session["ComputerScore"] as int? ?? 0;

            return View(vm);
        }

        public ActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();

            if (model.Result == GameResult.None)
            {
                if (_service.MakeMove(model, row, column, CellState.X))
                {
                    if (model.Result == GameResult.None)
                    {
                        _service.MakeComputerMove(model);
                    }

                    if (model.Result == GameResult.PlayerWin)
                    {
                        Session["PlayerScore"] = (Session["PlayerScore"] as int? ?? 0) + 1;
                    }
                    else if (model.Result == GameResult.ComputerWin)
                    {
                        Session["ComputerScore"] = (Session["ComputerScore"] as int? ?? 0) + 1;
                    }
                }
            }

            SaveGameModel(model);
            return RedirectToAction("Index");
        }


        public ActionResult NewGame()
        {
            var model = new GameModel();
            _service.Initialize(model);
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
            {
                var model = new GameModel();
                _service.Initialize(model);
                Session["GameModel"] = model;
                return model;
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
                IsGameOver = model.Result != GameResult.None
            };

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    result.Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        State = model[r, c],
                        IsWinningCell = model.WinningCells.Contains((r, c))
                    });
                }
            }

            if (model.Result == GameResult.PlayerWin) result.StatusText = "Вы победили";
            else if (model.Result == GameResult.ComputerWin) result.StatusText = "Компьютер победил";
            else if (model.Result == GameResult.Draw) result.StatusText = "Ничья";
            else result.StatusText = "Ваш ход (X)";

            return result;
        }
    }
}