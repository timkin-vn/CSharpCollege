using System.Text.Json;
using LightsOutGame.Business.Models;
using LightsOutGame.Business.Services;
using LightsOutGame.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightsOutGame.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _service = new GameService();
        private const string SessionKey = "GameModel";

        // GET: Game — новая перемешанная игра (аналог Index у преподавателя)
        public IActionResult Index()
        {
            var model = new GameModel();
            _service.Shuffle(model);
            SaveGameModel(model);

            return View(ToViewModel(model));
        }

        // Ход игрока по клетке (аналог MakeMove у преподавателя)
        public IActionResult MakeMove(int row, int column)
        {
            var model = GetGameModel();
            _service.MakeMove(model, row, column);
            SaveGameModel(model);

            if (_service.IsGameOver(model))
            {
                ViewBag.IsGameOver = true;
            }

            return View("Index", ToViewModel(model));
        }

        // --- работа с состоянием в Session (как Session["GameModel"] у преподавателя) ---

        private GameModel GetGameModel()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json))
            {
                var fresh = new GameModel();
                _service.Shuffle(fresh);
                SaveGameModel(fresh);
                return fresh;
            }

            return JsonSerializer.Deserialize<GameModel>(json) ?? new GameModel();
        }

        private void SaveGameModel(GameModel model)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(model));
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        IsOn = model[row, column],
                    };
                }
            }

            return result;
        }
    }
}
