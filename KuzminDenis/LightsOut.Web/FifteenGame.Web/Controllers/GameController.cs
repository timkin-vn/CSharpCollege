using LightsOut.Business.Models;
using LightsOut.Business.Service;
using LightsOut.Web.Models;
using System;
using System.Web.Mvc;

namespace LightsOut.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetModel();
            _service.Shuffle(model);
            SaveModel(model);

            return View(ToViewModel(model));
        }

        public ActionResult ClickCell(int row, int column)
        {
            var model = GetModel();

            _service.MakeMoveAt(model, row, column);
            model.MovesLeft--;
            SaveModel(model);

            var vm = ToViewModel(model);
            vm.IsGameOver = _service.IsGameOver(model);
            vm.IsGameLose = _service.IsGameLose(model);

            return View("Index", vm);
        }

        private GameModel GetModel()
        {
            if (Session["LightsOutModel"] == null)
                Session["LightsOutModel"] = new GameModel();

            return (GameModel)Session["LightsOutModel"];
        }

        private void SaveModel(GameModel model)
        {
            Session["LightsOutModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var vm = new GameViewModel();

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    vm.Cells[r, c] = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        IsOn = model[r, c]
                    };
                }
            }
            vm.MovesLeft = model.MovesLeft;
            vm.MaxMoves = model.MaxMoves;

            return vm;
        }
    }
}
