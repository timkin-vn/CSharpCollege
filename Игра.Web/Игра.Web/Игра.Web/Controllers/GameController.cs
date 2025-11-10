using System.Web.Mvc;
using Игра.Business.Models;
using Игра.Business.Services;
using Игра.Web.Models;

namespace Игра.Web.Controllers
{
    public class GameController : Controller
    {
        private static readonly GameModel _model = new GameModel();
        private static readonly GameService _service = new GameService(_model);

        public ActionResult Index()
        {
            ViewBag.IsGameWon = _service.CheckForWin();
            return View(ToViewModel(_model));
        }

        public ActionResult Click(int row, int col)
        {
            _service.Toggle(row, col);

            if (_service.CheckForWin())
            {
                ViewBag.IsGameWon = true;
            }

            return View("Index", ToViewModel(_model));
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var viewModel = new GameViewModel();
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    viewModel.Cells[r, c] = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        IsActive = model.Cells[r, c]
                    };
                }
            }
            return viewModel;
        }
    }
}
