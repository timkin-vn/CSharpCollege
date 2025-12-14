
using System.Web.Mvc;
using StepByStepPacman.Business.Services;
using StepByStepPacman.Business.Models;
using System;
using System.Linq;

namespace Pacman.web.Controllers
{
    public class GameController : Controller
    {
        private static GameService _service = new GameService();

        public ActionResult Index() => View(_service.State);

        [HttpPost]
        public JsonResult Move(string direction)
        {
            if (Enum.TryParse<Direction>(direction, true, out var dir))
            {
                _service.State.Player.Direction = dir;
                _service.Update();
            }

            var s = _service.State;

            

            int rows = s.GameBoard.GetLength(0); 
            int cols = s.GameBoard.GetLength(1); 

            
            int[][] boardJagged = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                boardJagged[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    boardJagged[i][j] = s.GameBoard[i, j];
                }
            }
            

            return Json(new
            {
                
                board = boardJagged,
                playerX = s.Player.X,
                playerY = s.Player.Y,
                ghosts = s.Ghosts.Select(g => new { g.X, g.Y, g.Color }),
                score = s.Score,
                lives = s.Lives,
                gameOver = s.IsGameOver,
                won = s.Won
            });
        }

        [HttpPost]
        public ActionResult Restart()
        {
            _service.Restart();
            return RedirectToAction("Index");
        }
    }
}