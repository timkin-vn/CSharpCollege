using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private readonly Random _random = new Random();

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();

            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel { UserId = userId };
            Initialize(game);
            var gameId = _repository.Save(ToDto(game));
            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
                for (int col = 0; col < Constants.ColumnCount; col++)
                    model[row, col] = 0;

            model.Score = 0;
            model.MoveCount = 0;
            model.IsWin = false;

            AddRandomTile(model);
            AddRandomTile(model);
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);

            if (model.IsWin)
                return model;

            // Сохраняем копию поля для сравнения
            var oldCells = new int[Constants.RowCount, Constants.ColumnCount];
            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount; j++)
                    oldCells[i, j] = model[i, j];

            int oldScore = model.Score;

            switch (direction)
            {
                case MoveDirection.Left: MoveLeft(model); break;
                case MoveDirection.Right: MoveRight(model); break;
                case MoveDirection.Up: MoveUp(model); break;
                case MoveDirection.Down: MoveDown(model); break;
                default: return model;
            }

            // Если поле не изменилось — ход не засчитываем
            if (AreEqual(oldCells, model) && oldScore == model.Score)
                return model;

            model.MoveCount++;
            AddRandomTile(model);

            // Проверка победы
            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount; j++)
                    if (model[i, j] == Constants.WinValue)
                        model.IsWin = true;

            _repository.Save(ToDto(model));
            return model;
        }

        private void MoveLeft(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                var line = new int[Constants.ColumnCount];
                for (int col = 0; col < Constants.ColumnCount; col++)
                    line[col] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int col = 0; col < Constants.ColumnCount; col++)
                    model[row, col] = newLine[col];

                model.Score += addedScore;
            }
        }

        private void MoveRight(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                var line = new int[Constants.ColumnCount];
                for (int col = 0; col < Constants.ColumnCount; col++)
                    line[Constants.ColumnCount - 1 - col] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int col = 0; col < Constants.ColumnCount; col++)
                    model[row, Constants.ColumnCount - 1 - col] = newLine[col];

                model.Score += addedScore;
            }
        }

        private void MoveUp(GameModel model)
        {
            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                var line = new int[Constants.RowCount];
                for (int row = 0; row < Constants.RowCount; row++)
                    line[row] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int row = 0; row < Constants.RowCount; row++)
                    model[row, col] = newLine[row];

                model.Score += addedScore;
            }
        }

        private void MoveDown(GameModel model)
        {
            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                var line = new int[Constants.RowCount];
                for (int row = 0; row < Constants.RowCount; row++)
                    line[Constants.RowCount - 1 - row] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int row = 0; row < Constants.RowCount; row++)
                    model[row, col] = newLine[Constants.RowCount - 1 - row];

                model.Score += addedScore;
            }
        }

        private (int[] newLine, int addedScore) MergeLine(int[] line)
        {
            var result = new int[line.Length];
            int writeIndex = 0;
            int scoreGain = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 0) continue;

                if (writeIndex > 0 && result[writeIndex - 1] == line[i])
                {
                    result[writeIndex - 1] *= 2;
                    scoreGain += result[writeIndex - 1];
                }
                else
                {
                    result[writeIndex] = line[i];
                    writeIndex++;
                }
            }

            return (result, scoreGain);
        }

        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new System.Collections.Generic.List<(int, int)>();
            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount; j++)
                    if (model[i, j] == 0)
                        emptyCells.Add((i, j));

            if (emptyCells.Count == 0) return;

            var (row, col) = emptyCells[_random.Next(emptyCells.Count)];
            model[row, col] = _random.Next(10) == 0 ? 4 : 2;
        }

        private bool AreEqual(int[,] oldCells, GameModel model)
        {
            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount; j++)
                    if (oldCells[i, j] != model[i, j])
                        return false;
            return true;
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            if (model.IsWin) return false;

            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount; j++)
                    if (model[i, j] == 0) return false;

            for (int i = 0; i < Constants.RowCount; i++)
                for (int j = 0; j < Constants.ColumnCount - 1; j++)
                    if (model[i, j] == model[i, j + 1]) return false;

            for (int j = 0; j < Constants.ColumnCount; j++)
                for (int i = 0; i < Constants.RowCount - 1; i++)
                    if (model[i, j] == model[i + 1, j]) return false;

            return true;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Score = dto.Score,
                MoveCount = dto.MoveCount,
                IsWin = dto.IsWin,
            };

            for (int row = 0; row < Constants.RowCount; row++)
                for (int col = 0; col < Constants.ColumnCount; col++)
                    result[row, col] = dto.Cells[row, col];

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Score = model.Score,
                MoveCount = model.MoveCount,
                IsWin = model.IsWin,
            };

            for (int row = 0; row < Constants.RowCount; row++)
                for (int col = 0; col < Constants.ColumnCount; col++)
                    result.Cells[row, col] = model[row, col];

            return result;
        }
    }
}