using _2048Game.Business.Models;
using _2048Game.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace _2048Game.Web.Controllers
{
    public class GameController : Controller
    {

        private static GameBoard _board = new GameBoard();
        private static readonly string HighScoreFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/highscore.txt");

        private static int LoadHighScore()
        {
            try
            {
                if (System.IO.File.Exists(HighScoreFile))
                {
                    var text = System.IO.File.ReadAllText(HighScoreFile);
                    return int.TryParse(text, out int score) ? score : 0;
                }
            }
            catch { }
            return 0;
        }

        private static void SaveHighScore(int score)
        {
            try
            {
                System.IO.File.WriteAllText(HighScoreFile, score.ToString());
            }
            catch { }
        }

        // GET: Game
        public ActionResult Index()
        {
            int highscore = LoadHighScore();
            var vm = new GameViewModel(_board)
            {
                HighScore = highscore
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Move(string direction)
        {
            if (Enum.TryParse(direction, true, out MoveDirection.Direction dir))
            {
                if (_board.Move(dir))
                {
                    var vm = new GameViewModel(_board);
                    int highscore = LoadHighScore();
                    if (vm.Score >  highscore)
                    {
                        highscore = vm.Score;
                        SaveHighScore(highscore);
                    }
                    vm.HighScore = highscore;
                    return View("Index", vm);
                }
            }
            var currentVm = new GameViewModel(_board)
            {
                HighScore = LoadHighScore()
            };
            return View("Index", currentVm);
        }

        public ActionResult Reset()
        {
            _board.Reset();
            var vm = new GameViewModel(_board)
            {
                HighScore = LoadHighScore()
            };
            return View("Index", vm);
        }
    }
}