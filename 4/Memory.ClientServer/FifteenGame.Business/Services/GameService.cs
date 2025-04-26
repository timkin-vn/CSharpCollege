using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly Random _rand = new Random();

        public GameService()
        {
            _gameRepository = new GameRepository();
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();

            if (dto != null)
                return FromDto(dto);

            var model = new GameModel
            {
                UserId = userId,
                GameStart = DateTime.Now,
                MoveCount = 0,
                Score = 0,
            };

            model.Reset(); 
            AddRandomTile(model);
            AddRandomTile(model);

            var gameId = _gameRepository.Save(ToDto(model));
            return GetByGameId(gameId);
        }

        public GameModel CheckMatch(int gameId, int[] moveParams, int[] unused = null)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            var model = FromDto(dto);

            bool moved = false;
            switch (moveParams[0])
            {
                case 0: moved = MoveUp(model); break;
                case 1: moved = MoveRight(model); break;
                case 2: moved = MoveDown(model); break;
                case 3: moved = MoveLeft(model); break;
            }

            if (moved)
            {
                AddRandomTile(model);
                model.MoveCount++;
                _gameRepository.Save(ToDto(model));
            }

            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            var model = FromDto(dto);
            return IsGameOver(model);
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        private void AddRandomTile(GameModel model)
        {
            var empty = new List<(int r, int c)>();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (model.Field[i, j] == 0)
                        empty.Add((i, j));
            if (empty.Count > 0)
            {
                var (r, c) = empty[_rand.Next(empty.Count)];
                model.Field[r, c] = _rand.Next(0, 10) == 0 ? 4 : 2;
            }
        }

        private bool MoveLeft(GameModel game)
        {
            bool moved = false;
            int score = game.Score;
            for (int i = 0; i < 4; i++)
            {
                int[] line = new int[4];
                for (int j = 0; j < 4; j++)
                    line[j] = game.Field[i, j];

                int[] merged = SlideAndMerge(line, ref moved, ref score);

                for (int j = 0; j < 4; j++)
                    game.Field[i, j] = merged[j];
            }
            game.Score = score;
            return moved;
        }

        private bool MoveRight(GameModel game)
        {
            bool moved = false;
            int score = game.Score;
            for (int i = 0; i < 4; i++)
            {
                int[] line = new int[4];
                for (int j = 0; j < 4; j++)
                    line[j] = game.Field[i, 3 - j];

                int[] merged = SlideAndMerge(line, ref moved, ref score);

                for (int j = 0; j < 4; j++)
                    game.Field[i, 3 - j] = merged[j];
            }
            game.Score = score;
            return moved;
        }

        private bool MoveUp(GameModel game)
        {
            bool moved = false;
            int score = game.Score;
            for (int j = 0; j < 4; j++)
            {
                int[] line = new int[4];
                for (int i = 0; i < 4; i++)
                    line[i] = game.Field[i, j];

                int[] merged = SlideAndMerge(line, ref moved, ref score);

                for (int i = 0; i < 4; i++)
                    game.Field[i, j] = merged[i];
            }
            game.Score = score;
            return moved;
        }

        private bool MoveDown(GameModel game)
        {
            bool moved = false;
            int score = game.Score;
            for (int j = 0; j < 4; j++)
            {
                int[] line = new int[4];
                for (int i = 0; i < 4; i++)
                    line[i] = game.Field[3 - i, j];

                int[] merged = SlideAndMerge(line, ref moved, ref score);

                for (int i = 0; i < 4; i++)
                    game.Field[3 - i, j] = merged[i];
            }
            game.Score = score;
            return moved;
        }

        private int[] SlideAndMerge(int[] line, ref bool moved, ref int score)
        {
            List<int> numbers = line.Where(x => x != 0).ToList();
            List<int> result = new List<int>();
            int skip = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (skip > 0)
                {
                    skip--;
                    continue;
                }
                if (i + 1 < numbers.Count && numbers[i] == numbers[i + 1])
                {
                    int merged = numbers[i] * 2;
                    result.Add(merged);
                    score += merged;
                    skip = 1;
                    moved = true;
                }
                else
                {
                    result.Add(numbers[i]);
                }
            }
            while (result.Count < 4)
                result.Add(0);

            for (int i = 0; i < 4; i++)
                if (line[i] != result[i]) moved = true;

            return result.ToArray();
        }

        private bool IsGameOver(GameModel game)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (game.Field[i, j] == 0)
                        return false;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (i < 3 && game.Field[i, j] == game.Field[i + 1, j])
                        return false;
                    if (j < 3 && game.Field[i, j] == game.Field[i, j + 1])
                        return false;
                }
            return true;
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null) return null;
            return new GameModel
            {
                GameId = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameStart = dto.GameStart,
                Field = (int[,])dto.Field.Clone(),
                Score = dto.Score,
            };
        }

        private GameDto ToDto(GameModel game)
        {
            return new GameDto
            {
                Id = game.GameId,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                GameStart = game.GameStart,
                Field = (int[,])game.Field.Clone(),
                Score = game.Score,
            };
        }
    }
}
