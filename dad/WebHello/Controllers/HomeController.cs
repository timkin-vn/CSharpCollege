using System.Collections.Generic;
using System.Web.Mvc;
using LabyrinthGame.Business.Models; // Подключаем нашу библиотеку
using LabyrinthGame.Models;          // Подключаем модели веба

namespace LabyrinthGame.Controllers
{
    public class HomeController : Controller
    {
        // Получить игру из сессии или создать новую
        private GameEngine GetGame()
        {
            if (Session["Game"] == null)
            {
                Session["Game"] = new GameEngine();
            }
            return (GameEngine)Session["Game"];
        }

        // Обнулить игру
        private void ResetGame()
        {
            Session["Game"] = new GameEngine();
        }

        // Главная страница
        public ActionResult Index()
        {
            ResetGame(); // При заходе на страницу начинаем новую игру
            var engine = GetGame();
            var model = MapToViewModel(engine, "Начните игру! Используйте стрелки.");
            return View(model);
        }

        // Обработка движения (вызывается через JavaScript)
        [HttpPost]
        public ActionResult Move(string direction)
        {
            var engine = GetGame();
            int dRow = 0, dCol = 0;

            switch (direction)
            {
                case "Up": dRow = -1; break;
                case "Down": dRow = 1; break;
                case "Left": dCol = -1; break;
                case "Right": dCol = 1; break;
                case "Restart":
                    ResetGame();
                    engine = GetGame();
                    break;
            }

            string msg = "";
            if (direction != "Restart")
            {
                var result = engine.MovePlayer(dRow, dCol);
                msg = result.Message;
            }
            else
            {
                msg = "Игра перезапущена";
            }

            // Возвращаем JSON с новым состоянием поля
            var model = MapToViewModel(engine, msg);
            return Json(model);
        }

        // Превращаем движок в красивые данные для HTML
        private GameViewModel MapToViewModel(GameEngine engine, string message)
        {
            var grid = new List<List<CellViewModel>>();

            for (int i = 0; i < engine.MazeSize; i++)
            {
                var row = new List<CellViewModel>();
                for (int j = 0; j < engine.MazeSize; j++)
                {
                    string cssClass = "path";
                    string symbol = "";

                    var cellType = engine.Maze[i, j];

                    if (i == engine.PlayerRow && j == engine.PlayerCol)
                    {
                        cssClass = "player";
                        symbol = "●";
                    }
                    else if (i == engine.ExitRow && j == engine.ExitCol)
                    {
                        cssClass = "exit";
                        symbol = "★";
                    }
                    else if (cellType == CellType.Wall)
                    {
                        cssClass = "wall";
                    }

                    row.Add(new CellViewModel { CssClass = cssClass, Symbol = symbol });
                }
                grid.Add(row);
            }

            return new GameViewModel
            {
                Grid = grid,
                Moves = engine.MovesCount,
                Message = message,
                IsFinished = engine.IsGameFinished
            };
        }
    }
}