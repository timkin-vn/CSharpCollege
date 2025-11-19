using Microsoft.AspNetCore.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGameWeb2.Controllers
{
    public class GameController : Controller
    {
        private static readonly GameField PlayerField = new GameField();
        private static readonly GameField ComputerField = new GameField();
        private static readonly GameService GameService = new GameService();

        private static int lastHitRow = -1;
        private static int lastHitCol = -1;
        private static bool huntingMode = false;

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
            bool hit = GameService.PlayerAttack(ComputerField, row, column);
            bool destroyed = hit && ComputerField.IsShipDestroyed(row, column);

            GameService.ComputerAttack(PlayerField, ref lastHitRow, ref lastHitCol, ref huntingMode);

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
            lastHitRow = lastHitCol = -1;
            huntingMode = false;
            GameService.Initialize(PlayerField, ComputerField);
            return RedirectToAction("Index");
        }
    }
}