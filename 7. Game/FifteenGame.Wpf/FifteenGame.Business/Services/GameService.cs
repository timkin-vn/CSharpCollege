using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = _random.Next(GameModel.GemTypes);
                    model.Matched[row, column] = false;
                }
            }

            model.Score = 0;
            model.MovesLeft = 20;
            model.GameState = GameState.Playing;
            model.SelectedRow = -1;
            model.SelectedColumn = -1;

            while (FindMatches(model).Count > 0)
            {
                RefillBoard(model);
            }
        }

        public bool SelectGem(GameModel model, int row, int column)
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

        private void SwapGems(GameModel model, int row1, int col1, int row2, int col2)
        {
            int temp = model[row1, col1];
            model[row1, col1] = model[row2, col2];
            model[row2, col2] = temp;
        }

        private List<Match> FindMatches(GameModel model)
        {
            var matches = new List<Match>();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount - 2; col++)
                {
                    int gemType = model[row, col];
                    if (gemType == model[row, col + 1] && gemType == model[row, col + 2])
                    {
                        int length = 3;
                        while (col + length < GameModel.ColumnCount && model[row, col + length] == gemType)
                            length++;

                        matches.Add(new Match { Row = row, Column = col, Length = length, IsHorizontal = true });
                        col += length - 1;
                    }
                }
            }

            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                for (int row = 0; row < GameModel.RowCount - 2; row++)
                {
                    int gemType = model[row, col];
                    if (gemType == model[row + 1, col] && gemType == model[row + 2, col])
                    {
                        int length = 3;
                        while (row + length < GameModel.RowCount && model[row + length, col] == gemType)
                            length++;

                        matches.Add(new Match { Row = row, Column = col, Length = length, IsHorizontal = false });
                        row += length - 1;
                    }
                }
            }

            return matches;
        }

        private void ProcessMatches(GameModel model, List<Match> matches)
        {
            foreach (var match in matches)
            {
                for (int i = 0; i < match.Length; i++)
                {
                    int row = match.IsHorizontal ? match.Row : match.Row + i;
                    int col = match.IsHorizontal ? match.Column + i : match.Column;
                    model.Matched[row, col] = true;
                }

                model.Score += match.Length * 10;
            }

            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                for (int row = GameModel.RowCount - 1; row >= 0; row--)
                {
                    if (model.Matched[row, col])
                    {
                        for (int r = row; r > 0; r--)
                        {
                            model[r, col] = model[r - 1, col];
                            model.Matched[r, col] = model.Matched[r - 1, col];
                        }
                        model[0, col] = _random.Next(GameModel.GemTypes);
                        model.Matched[0, col] = false;
                    }
                }
            }

            var newMatches = FindMatches(model);
            if (newMatches.Count > 0)
            {
                ProcessMatches(model, newMatches);
            }
        }

        private void RefillBoard(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = _random.Next(GameModel.GemTypes);
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