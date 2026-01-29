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
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly Random _rnd = new Random();

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public void Initialize(GameModel model)
        {
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model[r, c] = 0;
                }
            }

            model.MoveCount = 0;
            model.Score = 0;
            model.IsWin = false;
            model.IsLose = false;

            AddRandomTile(model);
            AddRandomTile(model);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (direction == MoveDirection.None) return false;
            if (model.IsWin || model.IsLose) return false;

            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Left:
                    moved = MoveLeft(model);
                    break;
                case MoveDirection.Right:
                    moved = MoveRight(model);
                    break;
                case MoveDirection.Up:
                    moved = MoveUp(model);
                    break;
                case MoveDirection.Down:
                    moved = MoveDown(model);
                    break;
            }

            if (moved)
            {
                model.MoveCount++;
                AddRandomTile(model);
            }

            model.IsWin = HasTile(model, 2048);
            model.IsLose = !model.IsWin && !HasAnyMoves(model);

            return moved;
        }

        public bool IsGameOver(GameModel model)
        {
            return model.IsWin || model.IsLose;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);

            GameModel model;
            if (dto == null)
            {
                model = new GameModel { Id = gameId };
                Initialize(model);
            }
            else
            {
                model = FromDto(dto);
            }

            MakeMove(model, direction);

            _repository.Save(ToDto(model));
            return model;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return false;
            return IsGameOver(FromDto(dto));
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
                UserId = userId
            };

            Initialize(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null) return null;
            return FromDto(dto);
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private void AddRandomTile(GameModel model)
        {
            var empties = new List<Tuple<int, int>>();

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    if (model[r, c] == 0)
                    {
                        empties.Add(Tuple.Create(r, c));
                    }
                }
            }

            if (empties.Count == 0) return;

            var chosen = empties[_rnd.Next(empties.Count)];
            int value = (_rnd.NextDouble() < 0.9) ? 2 : 4;

            model[chosen.Item1, chosen.Item2] = value;
        }

        private bool HasTile(GameModel model, int value)
        {
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    if (model[r, c] == value)
                        return true;

            return false;
        }

        private bool HasAnyMoves(GameModel model)
        {
            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    if (model[r, c] == 0)
                        return true;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    int v = model[r, c];
                    if (c + 1 < Constants.ColumnCount && model[r, c + 1] == v) return true;
                    if (r + 1 < Constants.RowCount && model[r + 1, c] == v) return true;
                }
            }

            return false;
        }

        private bool MoveLeft(GameModel model)
        {
            bool changed = false;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                int[] line = new int[Constants.ColumnCount];
                for (int c = 0; c < Constants.ColumnCount; c++)
                    line[c] = model[r, c];

                var merged = CompressAndMerge(line);
                int[] newLine = merged.Item1;
                int gained = merged.Item2;
                bool lineChanged = merged.Item3;

                for (int c = 0; c < Constants.ColumnCount; c++)
                    model[r, c] = newLine[c];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveRight(GameModel model)
        {
            bool changed = false;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                int[] line = new int[Constants.ColumnCount];
                for (int c = 0; c < Constants.ColumnCount; c++)
                    line[c] = model[r, Constants.ColumnCount - 1 - c];

                var merged = CompressAndMerge(line);
                int[] newLine = merged.Item1;
                int gained = merged.Item2;
                bool lineChanged = merged.Item3;

                for (int c = 0; c < Constants.ColumnCount; c++)
                    model[r, Constants.ColumnCount - 1 - c] = newLine[c];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveUp(GameModel model)
        {
            bool changed = false;

            for (int c = 0; c < Constants.ColumnCount; c++)
            {
                int[] line = new int[Constants.RowCount];
                for (int r = 0; r < Constants.RowCount; r++)
                    line[r] = model[r, c];

                var merged = CompressAndMerge(line);
                int[] newLine = merged.Item1;
                int gained = merged.Item2;
                bool lineChanged = merged.Item3;

                for (int r = 0; r < Constants.RowCount; r++)
                    model[r, c] = newLine[r];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveDown(GameModel model)
        {
            bool changed = false;

            for (int c = 0; c < Constants.ColumnCount; c++)
            {
                int[] line = new int[Constants.RowCount];
                for (int r = 0; r < Constants.RowCount; r++)
                    line[r] = model[Constants.RowCount - 1 - r, c];

                var merged = CompressAndMerge(line);
                int[] newLine = merged.Item1;
                int gained = merged.Item2;
                bool lineChanged = merged.Item3;

                for (int r = 0; r < Constants.RowCount; r++)
                    model[Constants.RowCount - 1 - r, c] = newLine[r];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private Tuple<int[], int, bool> CompressAndMerge(int[] line)
        {
            List<int> nonZero = line.Where(x => x != 0).ToList();
            List<int> result = new List<int>(line.Length);

            int gained = 0;
            bool changed = false;

            for (int i = 0; i < nonZero.Count; i++)
            {
                if (i + 1 < nonZero.Count && nonZero[i] == nonZero[i + 1])
                {
                    int v = nonZero[i] * 2;
                    result.Add(v);
                    gained += v;
                    i++;
                    changed = true;
                }
                else
                {
                    result.Add(nonZero[i]);
                }
            }

            while (result.Count < line.Length)
                result.Add(0);

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != result[i])
                {
                    changed = true;
                    break;
                }
            }

            return Tuple.Create(result.ToArray(), gained, changed);
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                Score = dto.Score,
                IsWin = dto.IsWin,
                IsLose = dto.IsLose
            };

            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    result[r, c] = dto.Cells[r, c];

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                Score = game.Score,
                IsWin = game.IsWin,
                IsLose = game.IsLose,
                Cells = new int[Constants.RowCount, Constants.ColumnCount]
            };

            for (int r = 0; r < Constants.RowCount; r++)
                for (int c = 0; c < Constants.ColumnCount; c++)
                    dto.Cells[r, c] = game[r, c];

            return dto;
        }
    }
}