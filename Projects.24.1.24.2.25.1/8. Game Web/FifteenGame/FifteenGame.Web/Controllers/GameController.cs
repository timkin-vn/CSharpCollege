using System;
using System.Web.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Web.Models;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKey = "MinesweeperGame";
        private const string DifficultyKey = "MinesweeperDifficulty";
        private const string MovesKey = "MinesweeperMoves";
        private readonly MinesweeperService _service = new MinesweeperService();

        public ActionResult Index()
        {
            var game = GetGame();
            if (game == null)
            {
                StartNewGame("easy", true);
                game = GetGame();
            }

            return View(ToViewModel(game));
        }

        [HttpPost]
        public ActionResult NewGame(string difficultyKey, string firstMoveMode)
        {
            StartNewGame(difficultyKey, firstMoveMode != "dangerous");
            return RedirectToAction("Index");
        }

        public ActionResult Reveal(int row, int column)
        {
            var game = GetGame();
            if (game == null)
            {
                return RedirectToAction("Index");
            }

            if (_service.OpenCell(game, row, column))
            {
                Session[MovesKey] = GetMoveCount() + 1;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ToggleFlag(int row, int column)
        {
            var game = GetGame();
            if (game == null)
            {
                return RedirectToAction("Index");
            }

            _service.ToggleFlag(game, row, column);
            return RedirectToAction("Index");
        }

        private MinesweeperGame GetGame()
        {
            return Session[SessionKey] as MinesweeperGame;
        }

        private int GetMoveCount()
        {
            return Session[MovesKey] is int ? (int)Session[MovesKey] : 0;
        }

        private void StartNewGame(string difficultyKey, bool safeFirstMove)
        {
            int rows = 9;
            int columns = 9;
            int mines = 10;

            if (difficultyKey == "medium")
            {
                rows = 12;
                columns = 12;
                mines = 25;
            }
            else if (difficultyKey == "hard")
            {
                rows = 16;
                columns = 16;
                mines = 50;
            }
            else
            {
                difficultyKey = "easy";
            }

            // Игра создаётся общей бизнес-логикой из проекта №7.
            Session[SessionKey] = _service.CreateGame(rows, columns, mines, safeFirstMove);
            Session[DifficultyKey] = difficultyKey;
            Session[MovesKey] = 0;
        }

        private GameViewModel ToViewModel(MinesweeperGame game)
        {
            var difficultyKey = Session[DifficultyKey] as string ?? "easy";
            string difficultyName = "Лёгкий";
            if (difficultyKey == "medium") difficultyName = "Средний";
            if (difficultyKey == "hard") difficultyName = "Тяжёлый";

            var result = new GameViewModel
            {
                RowCount = game.Rows,
                ColumnCount = game.Columns,
                MineCount = game.MinesCount,
                OpenedCount = game.OpenedCells,
                FlagCount = game.FlagsCount,
                MoveCount = GetMoveCount(),
                DifficultyKey = difficultyKey,
                DifficultyName = difficultyName,
                SafeFirstMove = game.SafeFirstMove,
                IsWin = game.IsWon,
                IsLose = game.IsFinished && !game.IsWon,
                Cells = new CellViewModel[game.Rows, game.Columns]
            };

            for (int row = 0; row < game.Rows; row++)
            {
                for (int column = 0; column < game.Columns; column++)
                {
                    MinesweeperCell cell = game[row, column];
                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        IsOpened = cell.IsOpened || (result.IsLose && cell.IsMine),
                        IsFlagged = cell.IsFlagged,
                        IsMine = cell.IsMine,
                        MinesAround = cell.MinesAround,
                        IsGameFinished = game.IsFinished
                    };
                }
            }

            return result;
        }
    }
}
