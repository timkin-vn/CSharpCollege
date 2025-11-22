using _2048Game.Common.BusinessModels;
using _2048Game.Common.Definitions;
using _2048Game.Common.Dto;
using _2048Game.Common.Repositories;
using _2048Game.Common.Services;
using _2048Game.DataAccess.Repository;
using System;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace _2048Game.Business.Services
{
    public class GameService : IGameService
    {
        public GameModel Board { get; set; }

        private readonly IGameRepository _gameRepository;

        public event Action BoardUpdated;
        public event Action GameOver;

        public GameService()
        {
            _gameRepository = new GameRepository();
        }

        public void StartGame(int userId)
        {
            var lastGameDto = _gameRepository.GetByUserId(userId).LastOrDefault();
            if (lastGameDto == null)
            {
                Board = new GameModel
                {
                    UserId = userId
                };
                Save();
            }
            else
            {
                Board = FromDto(lastGameDto);
            }  
            BoardUpdated?.Invoke();
        }

        public void Restart()
        {
            if (Board != null)
            {
                Board.Reset();
                Save();
                BoardUpdated?.Invoke();
            }
        }
        
        private GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId
            };
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    dto.Cells[r, c] = model.Tiles[r, c];
                }
            }
            return dto;
        }
        private GameModel FromDto(GameDto dto)
        {
            var model = new GameModel()
            {
                Id = dto.Id,
                UserId = dto.UserId
            };
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model.Tiles[r, c] = dto.Cells[r, c];
                }
            }
            return model;
        }
        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            if (dto == null)
            {
                return null;
            }
            return FromDto(dto);
        }
        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto == null)
            {
                var newGame = new GameModel
                {
                    UserId = userId
                };
                var newDto = ToDto(newGame);
                int gameId = _gameRepository.Save(newDto);
                return GetByGameId(gameId);
            }
            return FromDto(dto);
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }

        public bool CheckGameOver(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            if (dto == null)
            {
                return true;
            }
            var model = FromDto(dto);
            return !model.CanMove();
        }

        public int Save(GameDto gameDto)
        {
            return _gameRepository.Save(gameDto);
        }

        public bool Move(MoveDirections.MoveDirection direction)
        {
            if (Board == null)
            {
                return false;
            }
            bool moved = Board.Move(direction);

            if (moved)
            {
                Save();
                BoardUpdated?.Invoke();
                if (!Board.CanMove())
                {
                    GameOver?.Invoke();
                }
            }
            return moved;
        }

        public int Save()
        {
            var dto = ToDto(Board);
            return _gameRepository.Save(dto);
        }
    }
}
