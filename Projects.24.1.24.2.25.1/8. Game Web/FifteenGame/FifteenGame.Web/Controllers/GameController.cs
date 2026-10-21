
using FifteenGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FifteenGame.Web.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKey = "2048GameState";
        private const int Size = 4;
        private readonly Random _random = new Random();

        [HttpGet]
        public ActionResult Index()
        {
            var state = GetState();
            return View(ToViewModel(state));
        }

        [HttpPost]
        public ActionResult NewGame()
        {
            var state = new GameState();
            AddRandomTile(state);
            AddRandomTile(state);
            SaveState(state);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MakeMove(string directionText)
        {
            if (string.IsNullOrWhiteSpace(directionText))
            {
                return RedirectToAction("Index");
            }

            MoveDirection direction;

            if (!Enum.TryParse(directionText, true, out direction) ||
                !Enum.IsDefined(typeof(MoveDirection), direction))
            {
                return RedirectToAction("Index");
            }

            GameState state = GetState();

            if (!state.IsGameOver)
            {
                MakeMoveInternal(state, direction);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Undo()
        {
            var state = GetState();

            if (state.CanUndo)
            {
                state.Board = (int[,])state.UndoBoard.Clone();
                state.Score = state.UndoScore;
                state.HasWon = state.UndoHasWon;
                state.IsGameOver = state.UndoIsGameOver;
                state.CanUndo = false;
            }

            SaveState(state);
            return RedirectToAction("Index");
        }

        private GameState GetState()
        {
            var state = Session[SessionKey] as GameState;

            if (state == null)
            {
                state = new GameState();
                AddRandomTile(state);
                AddRandomTile(state);
                SaveState(state);
            }

            return state;
        }

        private void SaveState(GameState state)
        {
            Session[SessionKey] = state;
        }

        private GameViewModel ToViewModel(GameState state)
        {
            var model = new GameViewModel
            {
                Score = state.Score,
                BestScore = Math.Max(state.BestScore, state.Score),
                CanUndo = state.CanUndo,
                HasWon = state.HasWon,
                IsGameOver = state.IsGameOver
            };

            state.BestScore = model.BestScore;

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    int value = state.Board[row, column];

                    model.Cells[row, column] = new CellViewModel
                    {
                        Value = value,
                        IsEmpty = value == 0
                    };
                }
            }

            SaveState(state);
            return model;
        }

        private void MakeMoveInternal(GameState state, MoveDirection direction)
        {
            int[,] previousBoard = (int[,])state.Board.Clone();
            int previousScore = state.Score;
            bool previousHasWon = state.HasWon;
            bool previousIsGameOver = state.IsGameOver;

            for (int line = 0; line < Size; line++)
            {
                var values = new List<int>();

                for (int position = 0; position < Size; position++)
                {
                    GetCoordinates(direction, line, position, out int row, out int column);
                    int value = state.Board[row, column];

                    if (value != 0)
                    {
                        values.Add(value);
                    }
                }

                var merged = new List<int>();

                for (int i = 0; i < values.Count; i++)
                {
                    if (i + 1 < values.Count && values[i] == values[i + 1])
                    {
                        int value = values[i] * 2;
                        merged.Add(value);
                        state.Score += value;
                        i++;
                    }
                    else
                    {
                        merged.Add(values[i]);
                    }
                }

                for (int position = 0; position < Size; position++)
                {
                    GetCoordinates(direction, line, position, out int row, out int column);
                    state.Board[row, column] = position < merged.Count
                        ? merged[position]
                        : 0;
                }
            }

            if (BoardsEqual(previousBoard, state.Board))
            {
                state.Score = previousScore;
                return;
            }

            state.UndoBoard = previousBoard;
            state.UndoScore = previousScore;
            state.UndoHasWon = previousHasWon;
            state.UndoIsGameOver = previousIsGameOver;
            state.CanUndo = true;

            AddRandomTile(state);

            if (ContainsValue(state.Board, 2048))
            {
                state.HasWon = true;
            }

            state.IsGameOver = IsBoardFullAndBlocked(state.Board);
            state.BestScore = Math.Max(state.BestScore, state.Score);
        }

        private void GetCoordinates(
            MoveDirection direction,
            int line,
            int position,
            out int row,
            out int column)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    row = line;
                    column = position;
                    break;

                case MoveDirection.Right:
                    row = line;
                    column = Size - 1 - position;
                    break;

                case MoveDirection.Up:
                    row = position;
                    column = line;
                    break;

                default:
                    row = Size - 1 - position;
                    column = line;
                    break;
            }
        }

        private void AddRandomTile(GameState state)
        {
            var emptyCells = new List<Tuple<int, int>>();

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (state.Board[row, column] == 0)
                    {
                        emptyCells.Add(Tuple.Create(row, column));
                    }
                }
            }

            if (emptyCells.Count == 0)
            {
                return;
            }

            var cell = emptyCells[_random.Next(emptyCells.Count)];
            state.Board[cell.Item1, cell.Item2] =
                _random.Next(10) == 0 ? 4 : 2;
        }

        private bool IsBoardFullAndBlocked(int[,] board)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (board[row, column] == 0)
                    {
                        return false;
                    }

                    if (row + 1 < Size &&
                        board[row, column] == board[row + 1, column])
                    {
                        return false;
                    }

                    if (column + 1 < Size &&
                        board[row, column] == board[row, column + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ContainsValue(int[,] board, int value)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (board[row, column] == value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool BoardsEqual(int[,] first, int[,] second)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (first[row, column] != second[row, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private enum MoveDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        private class GameState
        {
            public int[,] Board { get; set; } = new int[Size, Size];
            public int Score { get; set; }
            public int BestScore { get; set; }
            public bool HasWon { get; set; }
            public bool IsGameOver { get; set; }
            public bool CanUndo { get; set; }
            public int[,] UndoBoard { get; set; } = new int[Size, Size];
            public int UndoScore { get; set; }
            public bool UndoHasWon { get; set; }
            public bool UndoIsGameOver { get; set; }
        }
    }
}
