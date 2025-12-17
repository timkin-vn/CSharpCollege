using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly Random _random = new Random();

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameModel StartNewGame(int userId, int minesCount)
        {
            var existingGame = _gameRepository.GetByUserId(userId)
                .FirstOrDefault(g => g.GameState == "Playing");

            if (existingGame != null)
            {
                _gameRepository.Remove(existingGame.Id);
            }

            var model = new GameModel
            {
                UserId = userId,
                MinesCount = minesCount,
                GameState = GameState.Playing,
                MoveCount = 0,
                FlagsCount = 0
            };

            InitializeField(model);
            var dto = ConvertToDto(model);
            model.Id = _gameRepository.Save(dto);
            return model;
        }

        public GameModel RevealCell(int gameId, int row, int column)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            if (gameDto == null) return null;

            var model = ConvertToModel(gameDto);
            if (model.GameState != GameState.Playing ||
                model.IsRevealed(row, column) ||
                model[row, column] == Constants.FlagCellValue)
                return model;

            model.SetRevealed(row, column, true);
            model.MoveCount++;

            if (model.HasMine(row, column))
            {
                model.GameState = GameState.GameOver;
                RevealAllMines(model);
            }
            else if (model[row, column] == 0)
            {
                RevealAdjacentCells(model, row, column);
            }

            CheckWinCondition(model);
            SaveGameState(model);
            return model;
        }

        public GameModel ToggleFlag(int gameId, int row, int column)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            if (gameDto == null) return null;

            var model = ConvertToModel(gameDto);
            if (model.GameState != GameState.Playing || model.IsRevealed(row, column))
                return model;

            if (model[row, column] == Constants.FlagCellValue)
            {
                model[row, column] = 0;
                model.FlagsCount--;
            }
            else if (model.FlagsCount < model.MinesCount)
            {
                model[row, column] = Constants.FlagCellValue;
                model.FlagsCount++;
            }

            SaveGameState(model);
            return model;
        }

        public GameModel GetCurrentGame(int userId)
        {
            var gameDto = _gameRepository.GetByUserId(userId)
                .FirstOrDefault(g => g.GameState == "Playing");
            return gameDto != null ? ConvertToModel(gameDto) : null;
        }

        public GameModel GetByGameId(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);
            return gameDto != null ? ConvertToModel(gameDto) : null;
        }

        public IEnumerable<GameModel> GetFinishedGames(int userId)
        {
            var gameDtos = _gameRepository.GetFinishedGamesByUserId(userId);
            return gameDtos.Select(ConvertToModel);
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        public void SaveGameState(GameModel model)
        {
            var dto = ConvertToDto(model);
            _gameRepository.Save(dto);
        }

        private void InitializeField(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = 0;
                    model.SetRevealed(row, column, false);
                    model.SetMine(row, column, false);
                }
            }

            int minesPlaced = 0;
            while (minesPlaced < model.MinesCount)
            {
                int row = _random.Next(Constants.RowCount);
                int column = _random.Next(Constants.ColumnCount);
                if (!model.HasMine(row, column))
                {
                    model.SetMine(row, column, true);
                    minesPlaced++;
                }
            }

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (!model.HasMine(row, column))
                    {
                        model[row, column] = CalculateAdjacentMines(model, row, column);
                    }
                    else
                    {
                        model[row, column] = Constants.MineCellValue;
                    }
                }
            }
        }

        private int CalculateAdjacentMines(GameModel model, int row, int column)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;
                    if (newRow >= 0 && newRow < Constants.RowCount &&
                        newColumn >= 0 && newColumn < Constants.ColumnCount &&
                        model.HasMine(newRow, newColumn))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void RevealAdjacentCells(GameModel model, int row, int column)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;
                    if (newRow >= 0 && newRow < Constants.RowCount &&
                        newColumn >= 0 && newColumn < Constants.ColumnCount &&
                        !model.IsRevealed(newRow, newColumn) &&
                        model[newRow, newColumn] != Constants.FlagCellValue)
                    {
                        model.SetRevealed(newRow, newColumn, true);
                        if (model[newRow, newColumn] == 0)
                        {
                            RevealAdjacentCells(model, newRow, newColumn);
                        }
                    }
                }
            }
        }

        private void RevealAllMines(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model.HasMine(row, column))
                    {
                        model.SetRevealed(row, column, true);
                    }
                }
            }
        }

        private void CheckWinCondition(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (!model.HasMine(row, column) && !model.IsRevealed(row, column))
                    {
                        return;
                    }
                }
            }
            model.GameState = GameState.Won;
        }

        private GameDto ConvertToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                MinesCount = model.MinesCount,
                FlagsCount = model.FlagsCount,
                GameState = model.GameState.ToString()
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = model[row, column];
                    dto.Revealed[row, column] = model.IsRevealed(row, column);
                    dto.Mines[row, column] = model.HasMine(row, column);
                }
            }
            return dto;
        }

        private GameModel ConvertToModel(GameDto dto)
        {
            var model = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                MinesCount = dto.MinesCount,
                FlagsCount = dto.FlagsCount,
                GameState = (GameState)Enum.Parse(typeof(GameState), dto.GameState)
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = dto.Cells[row, column];
                    model.SetRevealed(row, column, dto.Revealed[row, column]);
                    model.SetMine(row, column, dto.Mines[row, column]);
                }
            }
            return model;
        }
    }
}