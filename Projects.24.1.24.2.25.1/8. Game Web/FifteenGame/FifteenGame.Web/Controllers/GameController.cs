using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private GameService GetGameService()
        {
            if (Session["GameService"] == null)
                Session["GameService"] = new GameService();
            return (GameService)Session["GameService"];
        }

        public ActionResult Index()
        {
            var service = GetGameService();
            return View(BuildViewModel(service));
        }

        [HttpPost]
        public ActionResult Move(int row, int col)
        {
            var service = GetGameService();
            service.MakeMove(row, col);
            return View("Index", BuildViewModel(service));
        }

        public ActionResult NewGame()
        {
            var service = GetGameService();
            service.StartNewGame();
            return View("Index", BuildViewModel(service));
        }

        private GameViewModel BuildViewModel(GameService service)
        {
            var vm = new GameViewModel();

            for (int i = 0; i < 9; i++)
            {
                int r = i / 3;
                int c = i % 3;
                int val = service.Model.Board[r, c];
                vm.Cells.Add(new CellViewModel
                {
                    Row = r,
                    Col = c,
                    Text = val == 1 ? "X" : val == 2 ? "O" : ""
                });
            }

            if (service.Model.Winner == 1) vm.Status = "Победили Крестики!";
            else if (service.Model.Winner == 2) vm.Status = "Победили Нолики!";
            else vm.Status = service.Model.CurrentPlayer == 1 ? "Ход: X" : "Ход: O";

            return vm;
        }
    }
}