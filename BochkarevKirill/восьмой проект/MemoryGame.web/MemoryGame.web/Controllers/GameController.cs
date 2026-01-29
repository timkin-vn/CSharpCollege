using System.Web.Mvc;
using MemoryGame.Business.Models;
using MemoryGame.Business.Services;
using MemoryGame.Web.ViewModels;

namespace MemoryGame.Web.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKey = "MEMORY_GAME";
        private readonly GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetOrCreateGame();
            return View(ToVm(model));
        }

        [HttpPost]
        public ActionResult NewGame()
        {
            var model = new MemoryGameModel();
            _service.Initialize(model);
            Session[SessionKey] = model;
            return PartialView("_Board", ToVm(model));
        }

        [HttpPost]
        public ActionResult Click(int row, int col)
        {
            var model = GetOrCreateGame();

            // пошагово: если висит неверная пара — следующий клик сначала закрывает
            if (model.NeedToHideMismatchedPair)
                _service.CommitTurn(model);
            else
                _service.PickCell(model, row, col);

            Session[SessionKey] = model;
            return PartialView("_Board", ToVm(model));
        }

        private MemoryGameModel GetOrCreateGame()
        {
            var model = Session[SessionKey] as MemoryGameModel;
            if (model == null)
            {
                model = new MemoryGameModel();
                _service.Initialize(model);
                Session[SessionKey] = model;
            }
            return model;
        }

        private GameVm ToVm(MemoryGameModel model)
        {
            var vm = new GameVm
            {
                IsWin = model.IsWin,
                IsGameOver = model.IsGameOver,
                NeedToHide = model.NeedToHideMismatchedPair
            };

            for (int r = 0; r < MemoryGameModel.RowCount; r++)
                for (int c = 0; c < MemoryGameModel.ColumnCount; c++)
                {
                    vm.Cells.Add(new CellVm
                    {
                        Row = r,
                        Col = c,
                        Value = model[r, c],
                        IsRevealed = model.IsRevealed(r, c),
                        IsMatched = model.IsMatched(r, c)
                    });
                }

            vm.StatusText = model.IsWin ? "Победа! Все пары найдены 🎉" : "Найди все пары!";
            return vm;
        }
    }
}
