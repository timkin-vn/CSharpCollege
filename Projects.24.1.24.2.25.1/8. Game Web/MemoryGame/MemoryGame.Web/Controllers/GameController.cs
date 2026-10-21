using MemoryGame.Business.Models;
using MemoryGame.Business.Services;
using MemoryGame.Web.Models;
using System.Web.Mvc;

namespace MemoryGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();

        // GET: Game
        public ActionResult Index()
        {
            var model = GetGameModel();
            return View(ToViewModel(model));
        }

        public ActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();
            if (!_service.IsGameOver(model))
            {
                _service.MakeMove(model, row, column);
                SaveGameModel(model);
            }

            // Перенаправление, чтобы обновление страницы не повторяло ход
            return RedirectToAction("Index");
        }

        public ActionResult Restart()
        {
            var model = new GameModel();
            _service.Shuffle(model);
            SaveGameModel(model);

            return RedirectToAction("Index");
        }
        private GameModel GetGameModel()
        {
            if (!(Session["GameModel"] is GameModel model))
            {
                model = new GameModel();
                _service.Shuffle(model);
                Session["GameModel"] = model;
            }

            return model;
        }

        private void SaveGameModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel
            {
                MoveCount = model.MoveCount,
                MatchedPairCount = model.MatchedPairCount,
                IsGameOver = _service.IsGameOver(model),
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = model[row, column],
                        State = model.GetState(row, column),
                        CanClick = !result.IsGameOver && _service.CanClick(model, row, column),
                    };
                }
            }

            return result;
        }
    }
}
