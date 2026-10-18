
using FifteenGame.Business.Models;
using System.Collections.Generic;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Stack<GameState> _history =
            new Stack<GameState>();

        public bool CanUndo => _history.Count > 0;

        public void Initialize(GameModel model)
        {
            int value = 1;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = value++;
                }
            }

            model[GameModel.RowCount - 1, GameModel.ColumnCount - 1] =
                GameModel.FreeCellValue;

            model.FreeCellRow = GameModel.RowCount - 1;
            model.FreeCellColumn = GameModel.ColumnCount - 1;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (!CanMakeMove(model, direction))
            {
                return false;
            }

            // Сохраняем состояние до выполнения хода.
            _history.Push(new GameState(model));

            switch (direction)
            {
                case MoveDirection.Left:
                    SwapWithFreeCell(
                        model,
                        model.FreeCellRow,
                        model.FreeCellColumn + 1);

                    model.FreeCellColumn++;
                    return true;

                case MoveDirection.Right:
                    SwapWithFreeCell(
                        model,
                        model.FreeCellRow,
                        model.FreeCellColumn - 1);

                    model.FreeCellColumn--;
                    return true;

                case MoveDirection.Up:
                    SwapWithFreeCell(
                        model,
                        model.FreeCellRow + 1,
                        model.FreeCellColumn);

                    model.FreeCellRow++;
                    return true;

                case MoveDirection.Down:
                    SwapWithFreeCell(
                        model,
                        model.FreeCellRow - 1,
                        model.FreeCellColumn);

                    model.FreeCellRow--;
                    return true;

                default:
                    _history.Pop();
                    return false;
            }
        }

        public bool Undo(GameModel model)
        {
            if (_history.Count == 0)
            {
                return false;
            }

            GameState previousState = _history.Pop();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = previousState.Cells[row, column];
                }
            }

            model.FreeCellRow = previousState.FreeCellRow;
            model.FreeCellColumn = previousState.FreeCellColumn;

            return true;
        }

        public void Shuffle(GameModel model)
        {
            // Новая игра не должна сохранять историю предыдущей.
            _history.Clear();

            Initialize(model);

            var random = new System.Random();

            for (int i = 0; i < 1000; i++)
            {
                var nextMove = (MoveDirection)(random.Next(4) + 1);
                MakeMove(model, nextMove);
            }

            // Перемешивание не считается пользовательскими ходами.
            _history.Clear();
        }

        public bool IsGameOver(GameModel model)
        {
            if (model.FreeCellRow != GameModel.RowCount - 1 ||
                model.FreeCellColumn != GameModel.ColumnCount - 1)
            {
                return false;
            }

            int value = 1;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (row == GameModel.RowCount - 1 &&
                        column == GameModel.ColumnCount - 1)
                    {
                        return model[row, column] == GameModel.FreeCellValue;
                    }

                    if (model[row, column] != value++)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CanMakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    return model.FreeCellColumn < GameModel.ColumnCount - 1;

                case MoveDirection.Right:
                    return model.FreeCellColumn > 0;

                case MoveDirection.Up:
                    return model.FreeCellRow < GameModel.RowCount - 1;

                case MoveDirection.Down:
                    return model.FreeCellRow > 0;

                default:
                    return false;
            }
        }

        private void SwapWithFreeCell(
            GameModel model,
            int tileRow,
            int tileColumn)
        {
            model[model.FreeCellRow, model.FreeCellColumn] =
                model[tileRow, tileColumn];

            model[tileRow, tileColumn] = GameModel.FreeCellValue;
        }

        private sealed class GameState
        {
            public int[,] Cells { get; }

            public int FreeCellRow { get; }

            public int FreeCellColumn { get; }

            public GameState(GameModel model)
            {
                Cells = new int[GameModel.RowCount, GameModel.ColumnCount];

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
                    {
                        Cells[row, column] = model[row, column];
                    }
                }

                FreeCellRow = model.FreeCellRow;
                FreeCellColumn = model.FreeCellColumn;
            }
        }
    }
}
