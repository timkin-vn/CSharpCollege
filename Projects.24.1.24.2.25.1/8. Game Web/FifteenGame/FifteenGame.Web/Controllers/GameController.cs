using FifteenGame.Web.Models;
using System;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private static readonly Random Random = new Random();
        private const string SessionKey = "MinesweeperGame";

        public ActionResult Index()
        {
            var model = GetGameModel();
            if (model == null)
            {
                model = CreateNewGame("easy", true);
                SaveGameModel(model);
            }

            return View(ToViewModel(model));
        }

        [HttpPost]
        public ActionResult NewGame(string difficultyKey, string firstMoveMode)
        {
            var model = CreateNewGame(difficultyKey, firstMoveMode != "dangerous");
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        public ActionResult Reveal(int row, int column)
        {
            var model = GetGameModel();
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            RevealCell(model, row, column);
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        public ActionResult ToggleFlag(int row, int column)
        {
            var model = GetGameModel();
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            ToggleCellFlag(model, row, column);
            SaveGameModel(model);
            return RedirectToAction("Index");
        }

        private MinesweeperGameModel GetGameModel()
        {
            return Session[SessionKey] as MinesweeperGameModel;
        }

        private void SaveGameModel(MinesweeperGameModel model)
        {
            Session[SessionKey] = model;
        }

        private MinesweeperGameModel CreateNewGame(string difficultyKey, bool safeFirstMove)
        {
            var rows = 9;
            var columns = 9;
            var mines = 10;
            var difficultyName = "Лёгкий";
            difficultyKey = string.IsNullOrWhiteSpace(difficultyKey) ? "easy" : difficultyKey;

            if (difficultyKey == "medium")
            {
                rows = 12;
                columns = 12;
                mines = 25;
                difficultyName = "Средний";
            }
            else if (difficultyKey == "hard")
            {
                rows = 16;
                columns = 16;
                mines = 50;
                difficultyName = "Тяжёлый";
            }
            else
            {
                difficultyKey = "easy";
            }

            var game = new MinesweeperGameModel
            {
                Rows = rows,
                Columns = columns,
                Mines = mines,
                DifficultyKey = difficultyKey,
                DifficultyName = difficultyName,
                SafeFirstMove = safeFirstMove,
                Cells = new MinesweeperCellModel[rows, columns]
            };

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    game.Cells[row, column] = new MinesweeperCellModel
                    {
                        Row = row,
                        Column = column
                    };
                }
            }

            if (!safeFirstMove)
            {
                PlaceMines(game, -1, -1);
                CalculateNumbers(game);
            }

            return game;
        }

        private void RevealCell(MinesweeperGameModel game, int row, int column)
        {
            if (game.IsWin || game.IsLose || !IsInside(game, row, column))
            {
                return;
            }

            var cell = game.Cells[row, column];
            if (cell.IsOpened || cell.IsFlagged)
            {
                return;
            }

            if (!game.MinesPlaced)
            {
                PlaceMines(game, row, column);
                CalculateNumbers(game);
            }

            game.MoveCount++;

            if (cell.IsMine)
            {
                cell.IsOpened = true;
                game.IsLose = true;
                OpenAllMines(game);
                return;
            }

            OpenSafeArea(game, row, column);
            CheckWin(game);
        }

        private void ToggleCellFlag(MinesweeperGameModel game, int row, int column)
        {
            if (game.IsWin || game.IsLose || !IsInside(game, row, column))
            {
                return;
            }

            var cell = game.Cells[row, column];
            if (cell.IsOpened)
            {
                return;
            }

            cell.IsFlagged = !cell.IsFlagged;
            game.FlagCount += cell.IsFlagged ? 1 : -1;
        }

        private void PlaceMines(MinesweeperGameModel game, int safeRow, int safeColumn)
        {
            var placed = 0;
            while (placed < game.Mines)
            {
                var row = Random.Next(game.Rows);
                var column = Random.Next(game.Columns);

                if (game.Cells[row, column].IsMine || (row == safeRow && column == safeColumn))
                {
                    continue;
                }

                game.Cells[row, column].IsMine = true;
                placed++;
            }

            game.MinesPlaced = true;
        }

        private void CalculateNumbers(MinesweeperGameModel game)
        {
            for (var row = 0; row < game.Rows; row++)
            {
                for (var column = 0; column < game.Columns; column++)
                {
                    var count = 0;
                    for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
                    {
                        for (var columnOffset = -1; columnOffset <= 1; columnOffset++)
                        {
                            if (rowOffset == 0 && columnOffset == 0)
                            {
                                continue;
                            }

                            var nearRow = row + rowOffset;
                            var nearColumn = column + columnOffset;
                            if (IsInside(game, nearRow, nearColumn) && game.Cells[nearRow, nearColumn].IsMine)
                            {
                                count++;
                            }
                        }
                    }

                    game.Cells[row, column].MinesAround = count;
                }
            }
        }

        private void OpenSafeArea(MinesweeperGameModel game, int row, int column)
        {
            if (!IsInside(game, row, column))
            {
                return;
            }

            var cell = game.Cells[row, column];
            if (cell.IsOpened || cell.IsFlagged || cell.IsMine)
            {
                return;
            }

            cell.IsOpened = true;
            game.OpenedCount++;

            if (cell.MinesAround > 0)
            {
                return;
            }

            for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (var columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                    {
                        continue;
                    }

                    OpenSafeArea(game, row + rowOffset, column + columnOffset);
                }
            }
        }

        private void OpenAllMines(MinesweeperGameModel game)
        {
            for (var row = 0; row < game.Rows; row++)
            {
                for (var column = 0; column < game.Columns; column++)
                {
                    if (game.Cells[row, column].IsMine)
                    {
                        game.Cells[row, column].IsOpened = true;
                    }
                }
            }
        }

        private void CheckWin(MinesweeperGameModel game)
        {
            if (game.OpenedCount >= game.Rows * game.Columns - game.Mines)
            {
                game.IsWin = true;
            }
        }

        private bool IsInside(MinesweeperGameModel game, int row, int column)
        {
            return row >= 0 && row < game.Rows && column >= 0 && column < game.Columns;
        }

        private GameViewModel ToViewModel(MinesweeperGameModel model)
        {
            var result = new GameViewModel
            {
                RowCount = model.Rows,
                ColumnCount = model.Columns,
                MineCount = model.Mines,
                OpenedCount = model.OpenedCount,
                FlagCount = model.FlagCount,
                MoveCount = model.MoveCount,
                DifficultyKey = model.DifficultyKey,
                DifficultyName = model.DifficultyName,
                SafeFirstMove = model.SafeFirstMove,
                IsWin = model.IsWin,
                IsLose = model.IsLose,
                Cells = new CellViewModel[model.Rows, model.Columns]
            };

            for (var row = 0; row < model.Rows; row++)
            {
                for (var column = 0; column < model.Columns; column++)
                {
                    var cell = model.Cells[row, column];
                    result.Cells[row, column] = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        IsOpened = cell.IsOpened,
                        IsFlagged = cell.IsFlagged,
                        IsMine = cell.IsMine,
                        MinesAround = cell.MinesAround,
                        IsGameFinished = model.IsWin || model.IsLose
                    };
                }
            }

            return result;
        }
    }
}
