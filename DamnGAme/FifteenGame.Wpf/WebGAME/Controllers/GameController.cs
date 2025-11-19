using Microsoft.AspNetCore.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGameWeb2.Controllers
{
    public class GameController : Controller
    {
        // Статические поля — одна игра на всех (для теста норм, потом можно в сессию)
        private static readonly GameField PlayerField = new();
        private static readonly GameField ComputerField = new();
        private static readonly GameService GameService = new();

        private static int lastHitRow = -1;
        private static int lastHitColumn = -1;
        private static bool huntingMode = false;

        // Инициализация при первом запуске
        static GameController()
        {
            GameService.Initialize(PlayerField, ComputerField);
        }

        public IActionResult Index()
        {
            ViewBag.PlayerField = PlayerField;
            ViewBag.ComputerField = ComputerField;
            ViewBag.PlayerShipsLeft = ComputerField.GetRemainingShips();
            ViewBag.ComputerShipsLeft = PlayerField.GetRemainingShips();
            return View();
        }

        [HttpPost]
        public JsonResult Shoot(int row, int column)
        {
            // Твоя оригинальная функция — ничего не меняем
            bool hit = GameService.PlayerAttack(ComputerField, row, column);
            bool destroyed = hit && ComputerField.IsShipDestroyed(row, column);

            // Ход компьютера — твоя оригинальная функция
            GameService.ComputerAttack(PlayerField, ref lastHitRow, ref lastHitColumn, ref huntingMode);

            return Json(new
            {
                hit,
                destroyed,
                playerWon = GameService.IsGameOver(ComputerField),
                computerWon = GameService.IsGameOver(PlayerField),
                playerShipsLeft = ComputerField.GetRemainingShips(),
                computerShipsLeft = PlayerField.GetRemainingShips()
            });
        }

        [HttpPost]
        public IActionResult Restart()
        {
            PlayerField.Clear();
            ComputerField.Clear();
            lastHitRow = lastHitColumn = -1;
            huntingMode = false;
            GameService.Initialize(PlayerField, ComputerField);
            return RedirectToAction("Index");
        }
    }
}