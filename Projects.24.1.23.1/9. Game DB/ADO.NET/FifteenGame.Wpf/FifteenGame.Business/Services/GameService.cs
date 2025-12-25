using FifteenGame.Business.Models;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        private const int GemTypeCount = 5;
        private static readonly Random _rnd = new Random();
        private const int FinishMatchesCount = 20;

        public void Initialize(GameModel model)
        {
            model.MatchesCount = 0;
            model.IsFinished = false;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = _rnd.Next(1, GemTypeCount);
                }
            }
            ClearInitialMatches(model);
        }

        public void AddMatches(GameModel model, int count)
        {
            model.MatchesCount += count;

            if (model.MatchesCount >= FinishMatchesCount)
            {
                model.IsFinished = true;
            }
        }

        private void ClearInitialMatches(GameModel model)
        {
            while (true)
            {
                var matches = CheckMatches(model);

                if (!matches.Any())
                    break;

                RemoveMatches(model, matches);
                ProcessMatches(model);
            }
        }

        public bool Swap(GameModel model, int r1, int c1, int r2, int c2)
        {
            if (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) != 1)
                return false;

            int temp = model[r1, c1];
            model[r1, c1] = model[r2, c2];
            model[r2, c2] = temp;

            var matches = CheckMatches(model);

            if (!matches.Any())
            {
                temp = model[r1, c1];
                model[r1, c1] = model[r2, c2];
                model[r2, c2] = temp;
                return false;
            }

            return true;
        }

        public List<(int row, int col)> CheckMatches(GameModel model)
        {
            var matches = new List<(int, int)>();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount - 2; col++)
                {
                    int val = model[row, col];
                    if (val != 0 && val == model[row, col + 1] && val == model[row, col + 2])
                    {
                        matches.Add((row, col));
                        matches.Add((row, col + 1));
                        matches.Add((row, col + 2));
                    }
                }
            }

            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                for (int row = 0; row < GameModel.RowCount - 2; row++)
                {
                    int val = model[row, col];
                    if (val != 0 && val == model[row + 1, col] && val == model[row + 2, col])
                    {
                        matches.Add((row, col));
                        matches.Add((row + 1, col));
                        matches.Add((row + 2, col));
                    }
                }
            }

            return matches;
        }

        public void RemoveMatches(GameModel model, List<(int row, int col)> matches)
        {
            foreach (var (r, c) in matches)
            {
                model[r, c] = 0;
            }
        }

        public void ProcessMatches(GameModel model)
        {
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                int writeRow = GameModel.RowCount - 1;

                for (int row = GameModel.RowCount - 1; row >= 0; row--)
                {
                    int current = model[row, col];
                    if (current != 0)
                    {
                        if (writeRow != row)
                        {
                            model[writeRow, col] = current;
                            model[row, col] = 0;
                        }
                        writeRow--;
                    }
                }

                for (int row = writeRow; row >= 0; row--)
                {
                    if (model[row, col] == 0)
                        model[row, col] = _rnd.Next(1, 6);
                }
            }
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
            };

            Initialize(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MatchesCount = dto.MatchesCount,
                IsFinished = dto.IsFinished,
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MatchesCount = game.MatchesCount,
                IsFinished = game.IsFinished
            };

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    dto.Cells[row, column] = game[row, column];
                }
            }

            return dto;
        }
    }
}
