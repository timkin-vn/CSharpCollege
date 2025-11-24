using Microsoft.AspNetCore.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace WebGAME.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var game = _gameService.CreateGame();
            TempData["GameId"] = game.Id;
            return View(game);
        }

        [HttpPost]
        public JsonResult Shoot(int x, int y)
        {
            var gameId = (string)TempData["GameId"];
            TempData["GameId"] = gameId; // сохраняем между запросами

            var game = _gameService.GetGame(gameId);
            if (game == null || game.IsOver)
                return Json(new { success = false, message = "Игра не найдена или завершена" });

            var result = _gameService.Shoot(gameId, x, y);

            var message = result switch
            {
                ShotResult.Miss => "Мимо!",
                ShotResult.Hit => "Попадание!",
                ShotResult.Sunk => "Потопил!",
                ShotResult.GameOver => $"Игра окончена! Победил: {(game.Winner == "Player" ? "Вы" : "Компьютер")}!",
                ShotResult.AlreadyShot => "Уже стреляли сюда!",
                _ => "Недопустимый выстрел"
            };

            return Json(new
            {
                success = true,
                result = result.ToString(),
                message,
                isOver = game.IsOver,
                winner = game.Winner,
                playerField = RenderField(game.PlayerField, revealShips: game.IsOver),
                opponentField = RenderField(game.OpponentField, revealShips: false)
            });
        }

        private string[,] RenderField(Field field, bool revealShips)
        {
            var result = new string[10, 10];
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    var cell = field.Cells[x, y];
                    if (!revealShips && cell.State == CellState.Ship)
                        result[x, y] = "empty";
                    else
                        result[x, y] = cell.State.ToString().ToLower();
                }
            return result;
        }
    }
}