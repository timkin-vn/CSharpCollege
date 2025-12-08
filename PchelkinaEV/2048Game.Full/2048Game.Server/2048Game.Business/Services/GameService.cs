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
                Board.Reset();
                Save();
            }
            else
            {
                Board = FromDto(lastGameDto);
            }  
            BoardUpdated?.Invoke();
        }

        public void Restart(int userId)
        {
            Board = new GameModel
            {
                UserId = userId
            };
            Board.Reset();
            Board.MoveCount = 0;
            Save();
            BoardUpdated?.Invoke();
        }
        
        private GameDto ToDto(GameModel model)
        {
            GameDto dto = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
            };
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    dto.Tiles[r, c] = model.Tiles[r, c];
                }
            }
            return dto;
        }
        private GameModel FromDto(GameDto dto)
        {
            var model = new GameModel()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount
            };
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model.Tiles[r, c] = dto.Tiles[r, c];
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
                return null;
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
        public bool Move(MoveDirections.MoveDirection direction)
        {
            if (Board == null)
            {
                return false;
            }
            bool moved = Board.Move(direction);
      
            if (moved)
            {
                Board.MoveCount++;
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
            int saveId = _gameRepository.Save(dto);
            Board.Id = saveId;
            return saveId;
        }
    }
}
