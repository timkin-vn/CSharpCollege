using Minesweeper.Business.Core;
using Minesweeper.Business.Mappers;
using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Repositories;
using Minesweeper.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public GameService(IGameRepository gameRepository, IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public GameResponse CreateGame(CreateGameRequest request)
        {
            try
            {

                var user = _userRepository.GetById(request.UserId);
                if (user == null)
                    throw new Exception("User not found");

                var field = new Field(request.Size, request.MineCount);

                var gameModel = new GameModel
                {
                    UserId = request.UserId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    FieldSize = request.Size,
                    MineCount = request.MineCount,
                    FieldData = field.Serialize(),
                    Status = "playing",
                    IsGameOver = false,
                    IsGameWon = false,
                    PlayTime = TimeSpan.Zero,
                    FlagsPlaced = 0,
                    CellsRevealed = 0
                };

                var gameDto = GameMapper.ToDto(gameModel);
                var createdGame = _gameRepository.Create(gameDto);

                _userRepository.UpdateLastPlayed(request.UserId);

                return GameMapper.ToResponse(GameMapper.ToModel(createdGame), field);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating game: {ex.Message}");
            }
        }

        public GameResponse GetGame(int gameId)
        {
            try
            {
                var gameDto = _gameRepository.GetById(gameId);
                if (gameDto == null)
                    throw new Exception("Game not found");

                var gameModel = GameMapper.ToModel(gameDto);
                var field = Field.Deserialize(gameDto.GameData);

                return GameMapper.ToResponse(gameModel, field);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting game: {ex.Message}");
            }
        }

        public IEnumerable<GameResponse> GetUserGames(int userId)
        {
            try
            {
                var games = _gameRepository.GetByUserId(userId);
                return games.Select(g =>
                {
                    var model = GameMapper.ToModel(g);
                    var field = Field.Deserialize(g.GameData);
                    return GameMapper.ToResponse(model, field);
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user games: {ex.Message}");
            }
        }

        public GameResponse MakeMove(MakeMoveRequest request)
        {
            try
            {
                var gameDto = _gameRepository.GetById(request.GameId);
                if (gameDto == null)
                    throw new Exception("Game not found");

                if (gameDto.IsGameOver || gameDto.IsGameWon)
                    throw new Exception("Game is already finished");

                var field = Field.Deserialize(gameDto.GameData);

                bool success;
                if (request.Action == "open")
                {
                    success = field.OpenCell(request.Row, request.Column);
                }
                else if (request.Action == "toggle_flag")
                {
                    success = field.ToggleFlag(request.Row, request.Column);
                }
                else
                {
                    throw new Exception("Invalid action");
                }

                if (!success)
                    throw new Exception("Move not allowed");

                var gameModel = GameMapper.ToModel(gameDto);
                gameModel.UpdatedAt = DateTime.Now;
                gameModel.PlayTime = gameDto.PlayTime; 
                gameModel.FieldData = field.Serialize();
                gameModel.IsGameOver = field.GameOver;
                gameModel.IsGameWon = field.GameWon;
                gameModel.Status = field.GameOver ? "game_over" : field.GameWon ? "won" : "playing";
                gameModel.FlagsPlaced = field.FlagsPlaced;
                gameModel.CellsRevealed = field.CellsRevealed;

                var updatedDto = GameMapper.ToDto(gameModel);
                _gameRepository.Update(updatedDto);

                if (field.GameOver || field.GameWon)
                {
                    _userRepository.UpdateStats(gameDto.UserId, field.GameWon);
                    _gameRepository.UpdateGameStats(gameDto.Id, gameDto.PlayTime, field.GameOver, field.GameWon);
                }

                return GameMapper.ToResponse(gameModel, field);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error making move: {ex.Message}");
            }
        }

        public GameResponse ToggleFlag(int gameId, int row, int column)
        {
            var request = new MakeMoveRequest
            {
                GameId = gameId,
                Row = row,
                Column = column,
                Action = "toggle_flag"
            };

            return MakeMove(request);
        }

        public void DeleteGame(int gameId)
        {
            try
            {
                _gameRepository.Delete(gameId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting game: {ex.Message}");
            }
        }

        public GameResponse GetLastActiveGame(int userId)
        {
            try
            {
                var gameDto = _gameRepository.GetLastActiveGame(userId);
                if (gameDto == null)
                    return null;

                var gameModel = GameMapper.ToModel(gameDto);
                var field = Field.Deserialize(gameDto.GameData);

                return GameMapper.ToResponse(gameModel, field);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting last active game: {ex.Message}");
            }
        }

        public bool IsGameOver(int gameId)
        {
            var gameDto = _gameRepository.GetById(gameId);
            return gameDto?.IsGameOver ?? false;
        }

        public bool IsGameWon(int gameId)
        {
            var gameDto = _gameRepository.GetById(gameId);
            return gameDto?.IsGameWon ?? false;
        }
    }
}