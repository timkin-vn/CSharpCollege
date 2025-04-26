using System.Web.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGame.Web.Controllers
{
    public class Game2048Controller : Controller
    {
        private static GameModel _model = new GameModel();
        private static GameService _service = new GameService(_model);



        [HttpPost]
        public ActionResult Reset()
        {
            _model.Reset();
            _service.StartNewGame();
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            int[,] grid = new int[GameModel.Size, GameModel.Size];
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    grid[i, j] = _model.GetCell(i, j);

            ViewBag.Grid = grid;
            ViewBag.Score = _model.Score;
            return View();
        }

        [HttpPost]
        public ActionResult Move(string direction)
        {
            switch (direction)
            {
                case "up": _service.Move(MoveDirection.Up); break;
                case "down": _service.Move(MoveDirection.Down); break;
                case "left": _service.Move(MoveDirection.Left); break;
                case "right": _service.Move(MoveDirection.Right); break;
            }

            return RedirectToAction("Index");
        }




    }
}