
using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private static readonly Random Random = new Random();

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    model[row, column] = GameModel.FreeCellValue;
                }
            }

            model.Score = 0;

            SpawnTile(model);
            SpawnTile(model);
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (direction == MoveDirection.None)
                return false;

            bool changed = false;

            for (int line = 0; line < GameModel.RowCount; line++)
            {
                int[] original = new int[GameModel.ColumnCount];

                for (int position = 0;
                     position < GameModel.ColumnCount;
                     position++)
                {
                    GetCoordinates(
                        direction,
                        line,
                        position,
                        out int row,
                        out int column);

                    original[position] = model[row, column];
                }

                var values = new List<int>();

                foreach (int value in original)
                {
                    if (value != GameModel.FreeCellValue)
                        values.Add(value);
                }

                var merged = new List<int>();

                for (int i = 0; i < values.Count; i++)
                {
                    if (i + 1 < values.Count &&
                        values[i] == values[i + 1])
                    {
                        int newValue = values[i] * 2;

                        merged.Add(newValue);
                        model.Score += newValue;
                        i++;
                    }
                    else
                    {
                        merged.Add(values[i]);
                    }
                }

                for (int position = 0;
                     position < GameModel.ColumnCount;
                     position++)
                {
                    int newValue = position < merged.Count
                        ? merged[position]
                        : GameModel.FreeCellValue;

                    GetCoordinates(
                        direction,
                        line,
                        position,
                        out int row,
                        out int column);

                    if (model[row, column] != newValue)
                        changed = true;

                    model[row, column] = newValue;
                }
            }

            if (changed)
                SpawnTile(model);

            return changed;
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
                    column = GameModel.ColumnCount - 1 - position;
                    break;

                case MoveDirection.Up:
                    row = position;
                    column = line;
                    break;

                case MoveDirection.Down:
                    row = GameModel.RowCount - 1 - position;
                    column = line;
                    break;

                default:
                    row = 0;
                    column = 0;
                    break;
            }
        }

        private void SpawnTile(GameModel model)
        {
            var freeCells = new List<Tuple<int, int>>();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    if (model[row, column] == GameModel.FreeCellValue)
                    {
                        freeCells.Add(Tuple.Create(row, column));
                    }
                }
            }

            if (freeCells.Count == 0)
                return;

            var cell = freeCells[Random.Next(freeCells.Count)];

            model[cell.Item1, cell.Item2] =
                Random.Next(10) == 0 ? 4 : 2;
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    int value = model[row, column];

                    if (value == GameModel.FreeCellValue)
                        return false;

                    if (column + 1 < GameModel.ColumnCount &&
                        value == model[row, column + 1])
                        return false;

                    if (row + 1 < GameModel.RowCount &&
                        value == model[row + 1, column])
                        return false;
                }
            }

            return true;
        }
    }
}
