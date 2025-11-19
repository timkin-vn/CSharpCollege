using Microsoft.AspNetCore.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using Battleship;

namespace FifteenGameWeb2.Controllers
{
    public class GameController : Controller
    {
        private static GameField PlayerField = new GameField();
        private static GameField ComputerField = new GameField();
        private static GameService GameService = new GameService();

        static GameController()
        {
            GameService.Initialize(PlayerField, ComputerField);
        }

        public IActionResult Index()
        {
            ViewBag.PlayerField = PlayerField.GetGridCopy();
            ViewBag.ComputerField = ComputerField.GetMaskedGridForOpponent();

            ViewBag.PlayerShipsLeft = GameService.CountShipsLeft(ComputerField);
            ViewBag.ComputerShipsLeft = GameService.CountShipsLeft(PlayerField);

            return View();
        }

        [HttpPost]
        public JsonResult Shoot(int row, int column)
        {
            bool hit = GameService.PlayerFire(ComputerField, row, column);
            bool destroyed = hit && GameService.IsShipDestroyed(ComputerField, row, column);

            bool playerWon = GameService.IsGameOver(ComputerField);
            bool computerWon = false;

            if (!playerWon)
            {
                GameService.ComputerFire(PlayerField);
                computerWon = GameService.IsGameOver(PlayerField);
            }

            return Json(new
            {
                hit = hit,
                destroyed = destroyed,
                playerWon = playerWon,
                computerWon = computerWon,
                playerShipsLeft = GameService.CountShipsLeft(ComputerField),
                computerShipsLeft = GameService.CountShipsLeft(PlayerField)
            });
        }

        [HttpPost]
        public IActionResult Restart()
        {
            PlayerField.Clear();
            ComputerField.Clear();
            GameService.Initialize(PlayerField, ComputerField);
            return RedirectToAction("Index");
        }
    }
}
