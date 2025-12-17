using FifteenGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private static GameViewModel _currentGame;
        private static Random _random = new Random();

        public ActionResult Index()
        {
            if (_currentGame == null)
            {
                InitializeGame(15);
            }

            return View(_currentGame);
        }

        [HttpPost]
        public ActionResult NewGame(int minesCount = 15)
        {
            InitializeGame(minesCount);
            return Json(_currentGame);
        }

        [HttpPost]
        public ActionResult RevealCell(int row, int column)
        {
            if (_currentGame.IsGameOver)
                return Json(_currentGame);

            var cell = _currentGame.Cells.First(c => c.Row == row && c.Column == column);

            if (cell.IsFlagged)
                return Json(_currentGame);

            cell.IsRevealed = true;

            if (cell.HasMine)
            {
                _currentGame.IsGameOver = true;
                _currentGame.GameStatus = "Игра окончена! Вы проиграли! Перезапуск через 3 секунды...";
                _currentGame.StatusColor = "red";
                cell.IsExploded = true;

                // Показываем все мины
                foreach (var mineCell in _currentGame.Cells.Where(c => c.HasMine && !c.IsRevealed))
                {
                    mineCell.IsRevealed = true;
                }

                return Json(_currentGame);
            }

            // Если клетка пустая, открываем соседние
            if (cell.AdjacentMines == 0)
            {
                RevealAdjacentCells(row, column);
            }

            CheckWinCondition();

            return Json(_currentGame);
        }

        [HttpPost]
        public ActionResult ToggleFlag(int row, int column)
        {
            if (_currentGame.IsGameOver)
                return Json(_currentGame);

            var cell = _currentGame.Cells.First(c => c.Row == row && c.Column == column);

            if (!cell.IsRevealed)
            {
                cell.IsFlagged = !cell.IsFlagged;
                _currentGame.FlagsCount += cell.IsFlagged ? 1 : -1;
            }

            return Json(_currentGame);
        }

        [HttpPost]
        public ActionResult ApplySettings(int minesCount)
        {
            minesCount = Math.Max(5, Math.Min(50, minesCount));
            InitializeGame(minesCount);
            return Json(_currentGame);
        }

        private void InitializeGame(int minesCount)
        {
            _currentGame = new GameViewModel
            {
                MinesCount = minesCount,
                TotalMines = minesCount,
                FlagsCount = 0,
                GameStatus = $"Игра начата! Мины: {minesCount}",
                StatusColor = "black",
                IsGameOver = false
            };

            // Создаем пустые клетки
            _currentGame.Cells.Clear();
            for (int row = 0; row < _currentGame.RowCount; row++)
            {
                for (int column = 0; column < _currentGame.ColumnCount; column++)
                {
                    _currentGame.Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        AdjacentMines = 0,
                        IsRevealed = false,
                        HasMine = false,
                        IsFlagged = false,
                        IsExploded = false
                    });
                }
            }

            // Расставляем мины
            PlaceMines(minesCount);

            // Рассчитываем соседние мины
            CalculateAdjacentMines();
        }

        private void PlaceMines(int minesCount)
        {
            int minesPlaced = 0;
            while (minesPlaced < minesCount)
            {
                int row = _random.Next(_currentGame.RowCount);
                int column = _random.Next(_currentGame.ColumnCount);

                var cell = _currentGame.Cells.First(c => c.Row == row && c.Column == column);
                if (!cell.HasMine)
                {
                    cell.HasMine = true;
                    minesPlaced++;
                }
            }
        }

        private void CalculateAdjacentMines()
        {
            foreach (var cell in _currentGame.Cells)
            {
                if (!cell.HasMine)
                {
                    cell.AdjacentMines = CountAdjacentMines(cell.Row, cell.Column);
                }
            }
        }

        private int CountAdjacentMines(int row, int column)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < _currentGame.RowCount &&
                        newColumn >= 0 && newColumn < _currentGame.ColumnCount)
                    {
                        var adjacentCell = _currentGame.Cells.First(c => c.Row == newRow && c.Column == newColumn);
                        if (adjacentCell.HasMine)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void RevealAdjacentCells(int row, int column)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < _currentGame.RowCount &&
                        newColumn >= 0 && newColumn < _currentGame.ColumnCount)
                    {
                        var adjacentCell = _currentGame.Cells.First(c => c.Row == newRow && c.Column == newColumn);
                        if (!adjacentCell.IsRevealed && !adjacentCell.IsFlagged && !adjacentCell.HasMine)
                        {
                            adjacentCell.IsRevealed = true;
                            if (adjacentCell.AdjacentMines == 0)
                            {
                                RevealAdjacentCells(newRow, newColumn);
                            }
                        }
                    }
                }
            }
        }

        private void CheckWinCondition()
        {
            bool allSafeCellsRevealed = _currentGame.Cells
                .Where(c => !c.HasMine)
                .All(c => c.IsRevealed);

            if (allSafeCellsRevealed)
            {
                _currentGame.IsGameOver = true;
                _currentGame.GameStatus = "Поздравляем! Вы выиграли!";
                _currentGame.StatusColor = "green";
            }
        }
    }
}