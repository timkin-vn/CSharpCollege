
using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private int GetCryptographicallyRandomNumber(int maxValue)
        {
            byte[] randomNumber = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            int value = BitConverter.ToInt32(randomNumber, 0);
            return Math.Abs(value % maxValue);
        }

        public void Initialize(GameField model)
        {
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    model[row, column] = GetCryptographicallyRandomNumber(GameField.GemTypes);
                    model.Matched[row, column] = false;
                }
            }

            model.Score = 0;
            model.MovesLeft = 20;
            model.GameState = GameState.Playing;
            model.SelectedRow = -1;
            model.SelectedColumn = -1;

            int attempts = 0;
            while (FindMatches(model).Count > 0 && attempts < 100)
            {
                for (int row = 0; row < GameField.RowCount; row++)
                {
                    for (int column = 0; column < GameField.ColumnCount; column++)
                    {
                        model[row, column] = GetCryptographicallyRandomNumber(GameField.GemTypes);
                    }
                }
                attempts++;
            }
        }

        public bool SelectGem(GameField model, int row, int column)
        {
            if (model.GameState != GameState.Playing)
                return false;

            if (model.SelectedRow == -1)
            {
                model.SelectedRow = row;
                model.SelectedColumn = column;
                return true;
            }

            if (IsAdjacent(model.SelectedRow, model.SelectedColumn, row, column))
            {
                SwapGems(model, model.SelectedRow, model.SelectedColumn, row, column);

                var matches = FindMatches(model);
                if (matches.Count == 0)
                {
                    SwapGems(model, model.SelectedRow, model.SelectedColumn, row, column);
                    model.SelectedRow = -1;
                    model.SelectedColumn = -1;
                    return false;
                }

                ProcessMatches(model, matches);

                while (FindMatches(model).Count > 0)
                {
                    ProcessMatches(model, FindMatches(model));
                }

                model.MovesLeft--;

                if (model.Score >= 1000)
                {
                    model.GameState = GameState.Won;
                }
                else if (model.MovesLeft <= 0)
                {
                    model.GameState = GameState.Lost;
                }

                model.SelectedRow = -1;
                model.SelectedColumn = -1;
                return true;
            }
            else
            {
                model.SelectedRow = row;
                model.SelectedColumn = column;
                return true;
            }
        }

        private bool IsAdjacent(int row1, int col1, int row2, int col2)
        {
            return (Math.Abs(row1 - row2) == 1 && col1 == col2) ||
                   (Math.Abs(col1 - col2) == 1 && row1 == row2);
        }

        private void SwapGems(GameField model, int row1, int col1, int row2, int col2)
        {
            int temp = model[row1, col1];
            model[row1, col1] = model[row2, col2];
            model[row2, col2] = temp;
        }

        private List<Match> FindMatches(GameField model)
        {
            var matches = new List<Match>();

            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int col = 0; col < GameField.ColumnCount - 2; col++)
                {
                    int gemType = model[row, col];
                    if (gemType >= 0 && gemType == model[row, col + 1] && gemType == model[row, col + 2])
                    {
                        int length = 3;
                        while (col + length < GameField.ColumnCount && model[row, col + length] == gemType)
                            length++;

                        matches.Add(new Match { Row = row, Column = col, Length = length, IsHorizontal = true });
                        col += length - 1;
                    }
                }
            }

            for (int col = 0; col < GameField.ColumnCount; col++)
            {
                for (int row = 0; row < GameField.RowCount - 2; row++)
                {
                    int gemType = model[row, col];
                    if (gemType >= 0 && gemType == model[row + 1, col] && gemType == model[row + 2, col])
                    {
                        int length = 3;
                        while (row + length < GameField.RowCount && model[row + length, col] == gemType)
                            length++;

                        matches.Add(new Match { Row = row, Column = col, Length = length, IsHorizontal = false });
                        row += length - 1;
                    }
                }
            }

            return matches;
        }

        private void ProcessMatches(GameField model, List<Match> matches)
        {
            foreach (var match in matches)
            {
                for (int i = 0; i < match.Length; i++)
                {
                    int row = match.IsHorizontal ? match.Row : match.Row + i;
                    int col = match.IsHorizontal ? match.Column + i : match.Column;
                    model.Matched[row, col] = true;
                    model[row, col] = GameField.EmptyCell;
                }

                model.Score += match.Length * 10;
            }

            for (int col = 0; col < GameField.ColumnCount; col++)
            {
                for (int row = GameField.RowCount - 1; row >= 0; row--)
                {
                    if (model[row, col] == GameField.EmptyCell)
                    {
                        int nonEmptyRow = row - 1;
                        while (nonEmptyRow >= 0 && model[nonEmptyRow, col] == GameField.EmptyCell)
                        {
                            nonEmptyRow--;
                        }

                        if (nonEmptyRow >= 0)
                        {
                            model[row, col] = model[nonEmptyRow, col];
                            model[nonEmptyRow, col] = GameField.EmptyCell;
                            model.Matched[row, col] = false;
                            model.Matched[nonEmptyRow, col] = false;
                        }
                        else
                        {
                            model[row, col] = GetCryptographicallyRandomNumber(GameField.GemTypes);
                            model.Matched[row, col] = false;
                        }
                    }
                }

                for (int row = 0; row < GameField.RowCount; row++)
                {
                    if (model[row, col] == GameField.EmptyCell)
                    {
                        model[row, col] = GetCryptographicallyRandomNumber(GameField.GemTypes);
                        model.Matched[row, col] = false;
                    }
                }
            }
        }

        private class Match
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int Length { get; set; }
            public bool IsHorizontal { get; set; }
        }
    }
}
