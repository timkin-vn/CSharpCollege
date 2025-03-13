using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;
using System;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private GameService _service = new GameService();

        public ActionResult Index()
        {
            var model = GetGameModel();
            model.ResetBoard(); 
            SaveGameModel(model);
            return View(ToViewModel(model));
        }

        public ActionResult MakeMove(int row, int col)
        {
            var model = GetGameModel();
            if (model.Board[row, col] == GameModel.EmptyCell)
            {
                model.Board[row, col] = GameModel.Player;

                if (model.CheckWin(GameModel.Player))
                {
                    ViewBag.GameOver = true;
                    ViewBag.Winner = "Player";
                }
                else
                {
                    var (botRow, botCol) = _service.GetBotMove(model);
                    if (botRow != -1 && botCol != -1)
                    {
                        model.Board[botRow, botCol] = GameModel.Bot;

                        if (model.CheckWin(GameModel.Bot))
                        {
                            ViewBag.GameOver = true;
                            ViewBag.Winner = "Bot";
                        }
                        else if (model.IsFull())
                        {
                            ViewBag.GameOver = true;
                            ViewBag.Winner = "Draw";
                        }
                    }
                }

                SaveGameModel(model);
            }

            return View("Index", ToViewModel(model));
        }

        private GameModel GetGameModel()
        {
            if (Session["GameModel"] == null)
            {
                Session["GameModel"] = new GameModel();
            }

            return (GameModel)Session["GameModel"];
        }

        private void SaveGameModel(GameModel model)
        {
            Session["GameModel"] = model;
        }

        private GameViewModel ToViewModel(GameModel model)
        {
            var result = new GameViewModel
            {
                Cells = new CellViewModel[GameModel.Size, GameModel.Size]
            };

            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    result.Cells[row, col] = new CellViewModel
                    {
                        NumText = model.Board[row, col].ToString(),
                        IsEmpty = model.Board[row, col] == GameModel.EmptyCell
                    };
                }
            }

            return result;
        }
    }
}