using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using System;
using System.Linq;

namespace LightsOutGame.Business.Services
{
    public class GameService : IGameService
    {
        private static readonly Random _random = new Random();

        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameModel GetByGameId(int gameId)
        {
            return FromDto(_gameRepository.GetByGameId(gameId));
        }

        public GameModel GetByUserId(int userId)
        {
            var gameDto = _gameRepository.GetByUserId(userId).FirstOrDefault();

            if (gameDto == null)
            {
                gameDto = CreateNewGame(userId);
                gameDto.Id = _gameRepository.Save(gameDto);
            }

            return FromDto(gameDto);
        }

        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);

            if (gameDto == null)
            {
                return false;
            }

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (gameDto.Cells[row, column] == Constants.LightOn)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var gameDto = _gameRepository.GetByGameId(gameId);

            if (gameDto == null)
            {
                return null;
            }

            Press(gameDto, row, column);
            gameDto.MoveCount++;
            _gameRepository.Save(gameDto);

            return FromDto(gameDto);
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        // Нажатие переключает саму ячейку и её ортогональных соседей.
        private void Press(GameDto game, int row, int column)
        {
            Toggle(game, row, column);
            Toggle(game, row - 1, column);
            Toggle(game, row + 1, column);
            Toggle(game, row, column - 1);
            Toggle(game, row, column + 1);
        }

        private void Toggle(GameDto game, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)
            {
                return;
            }

            game.Cells[row, column] = game.Cells[row, column] == Constants.LightOn
                ? Constants.LightOff
                : Constants.LightOn;
        }

        // Поле, собранное из решённого состояния случайными нажатиями, всегда решаемо.
        private GameDto CreateNewGame(int userId)
        {
            var game = new GameDto
            {
                UserId = userId,
                MoveCount = 0,
            };

            int shuffleCount = _random.Next(5, 15);
            for (int i = 0; i < shuffleCount; i++)
            {
                Press(game, _random.Next(Constants.RowCount), _random.Next(Constants.ColumnCount));
            }

            return game;
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                }
            }

            return result;
        }
    }
}
