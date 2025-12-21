using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2048Game.Web.Models;

namespace _2048Game.Web.Controllers
{
    public class GameController : Controller
    {
        private static SokobanBoard _board = new SokobanBoard();

        // GET: Game
        public ActionResult Index()
        {
            var vm = new GameViewModel(_board);
            return View(vm);
        }        [HttpPost]        public ActionResult Move(string direction)        {            if (Enum.TryParse(direction, true, out Direction dir))            {                if (_board.Move(dir))                {                    var vm = new GameViewModel(_board);                    return View("Index", vm);                }            }            var currentVm = new GameViewModel(_board);            return View("Index", currentVm);        }        public ActionResult Reset()        {            _board = new SokobanBoard();            var vm = new GameViewModel(_board);            return View("Index", vm);        }    }}