using FifteenGame.Common.Dto;
using FifteenGame.Common.Enums;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace FifteenGame.Business.Services
{
    public class ServerGameService : IGameService
    {
        private readonly GameRepository _gameRepo = new GameRepository();
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly Random _rnd = new Random();

        private const int Rows = 8;
        private const int Cols = 8;
        private const int GemTypes = 6;

        public GameDto CreateGame(int userId, int mode)
        {
            var dto = new GameDto
            {
                UserId = userId,
                Mode = (GameMode)mode,
                Score = 0,
                MovesLeft = 20,
                State = GameState.Playing,
                Gems = new int[Rows * Cols],
                Matched = new bool[Rows * Cols]
            };

            FillBoard(dto);
            while (FindMatches(dto).Count > 0) FillBoard(dto);

            return _gameRepo.Save(dto);
        }

        public GameDto GetGame(int gameId) => _gameRepo.Get(gameId);

        public GameDto MakeMove(int gameId, int row, int col)
        {
            var model = _gameRepo.Get(gameId);
            if (model == null || model.State != GameState.Playing) return model;

            if (model.SelectedRow == -1)
            {
                model.SelectedRow = row;
                model.SelectedColumn = col;
                return _gameRepo.Save(model);
            }

            if (model.SelectedRow == row && model.SelectedColumn == col)
            {
                model.SelectedRow = -1;
                model.SelectedColumn = -1;
                return _gameRepo.Save(model);
            }

            if (IsAdjacent(model.SelectedRow, model.SelectedColumn, row, col))
            {
                Swap(model, model.SelectedRow, model.SelectedColumn, row, col);
                var matches = FindMatches(model);

                if (matches.Count == 0)
                {
                    Swap(model, model.SelectedRow, model.SelectedColumn, row, col);
                }
                else
                {
                    ProcessMatches(model, matches);
                    model.MovesLeft--;
                    CheckGameState(model);
                }
            }

            model.SelectedRow = -1;
            model.SelectedColumn = -1;

            return _gameRepo.Save(model);
        }

        private void CheckGameState(GameDto model)
        {
            if (model.Mode == GameMode.Classic)
            {
                if (model.Score >= 1000) model.State = GameState.Won;
                else if (model.MovesLeft <= 0) model.State = GameState.Lost;
            }
            else
            {
                if (model.MovesLeft <= 0)
                {
                    model.State = GameState.Lost;
                    _userRepo.UpdateScore(model.UserId, model.Score);
                }
            }
        }

        private void ProcessMatches(GameDto model, List<Match> matches)
        {
            foreach (var m in matches)
            {
                for (int i = 0; i < m.Length; i++)
                {
                    int r = m.IsHorizontal ? m.Row : m.Row + i;
                    int c = m.IsHorizontal ? m.Column + i : m.Column;
                    model.Matched[r * Cols + c] = true;
                }
                model.Score += m.Length * 10;
            }

            ApplyGravity(model);

            var newMatches = FindMatches(model);
            if (newMatches.Count > 0) ProcessMatches(model, newMatches);
        }

        private void ApplyGravity(GameDto model)
        {
            for (int c = 0; c < Cols; c++)
            {
                for (int r = Rows - 1; r >= 0; r--)
                {
                    if (model.Matched[r * Cols + c])
                    {
                        for (int k = r; k > 0; k--)
                        {
                            int currentIdx = k * Cols + c;
                            int aboveIdx = (k - 1) * Cols + c;
                            model.Gems[currentIdx] = model.Gems[aboveIdx];
                            model.Matched[currentIdx] = model.Matched[aboveIdx];
                        }
                        model.Gems[c] = _rnd.Next(GemTypes);
                        model.Matched[c] = false;
                        r++;
                    }
                }
            }
        }

        private void FillBoard(GameDto model)
        {
            for (int i = 0; i < model.Gems.Length; i++)
            {
                model.Gems[i] = _rnd.Next(GemTypes);
                model.Matched[i] = false;
            }
        }

        private bool IsAdjacent(int r1, int c1, int r2, int c2)
        {
            return (Math.Abs(r1 - r2) == 1 && c1 == c2) || (Math.Abs(c1 - c2) == 1 && r1 == r2);
        }

        private void Swap(GameDto model, int r1, int c1, int r2, int c2)
        {
            int idx1 = r1 * Cols + c1;
            int idx2 = r2 * Cols + c2;
            int temp = model.Gems[idx1];
            model.Gems[idx1] = model.Gems[idx2];
            model.Gems[idx2] = temp;
        }

        private List<Match> FindMatches(GameDto model)
        {
            var matches = new List<Match>();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols - 2; c++)
                {
                    int type = model.Gems[r * Cols + c];
                    if (type == model.Gems[r * Cols + (c + 1)] && type == model.Gems[r * Cols + (c + 2)])
                    {
                        int len = 3;
                        while (c + len < Cols && model.Gems[r * Cols + (c + len)] == type) len++;
                        matches.Add(new Match { Row = r, Column = c, Length = len, IsHorizontal = true });
                        c += len - 1;
                    }
                }
            }
            for (int c = 0; c < Cols; c++)
            {
                for (int r = 0; r < Rows - 2; r++)
                {
                    int type = model.Gems[r * Cols + c];
                    if (type == model.Gems[(r + 1) * Cols + c] && type == model.Gems[(r + 2) * Cols + c])
                    {
                        int len = 3;
                        while (r + len < Rows && model.Gems[(r + len) * Cols + c] == type) len++;
                        matches.Add(new Match { Row = r, Column = c, Length = len, IsHorizontal = false });
                        r += len - 1;
                    }
                }
            }
            return matches;
        }

        private class Match { public int Row; public int Column; public int Length; public bool IsHorizontal; }
    }
}