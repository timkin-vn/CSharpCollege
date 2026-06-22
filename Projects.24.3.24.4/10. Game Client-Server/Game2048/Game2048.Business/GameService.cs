using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;

namespace Game2048.Business
{

    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByUserId(int userId)
        {
            var game = _repository.GetByUserId(userId);
            if (game == null)
            {
                game = GameLogic.Initialize();
                game.UserId = userId;
                _repository.SaveGame(game);
            }
            return game;
        }

        public GameModel MakeMove(int userId, MoveDirection direction)
        {
            var game = GetByUserId(userId);
            if (!GameLogic.IsWon(game) && !GameLogic.IsGameOver(game))
            {
                if (GameLogic.MakeMove(game, direction))
                    _repository.SaveGame(game);
            }
            return game;
        }

        public bool IsWon(int userId)
        {
            return GameLogic.IsWon(GetByUserId(userId));
        }

        public bool IsGameOver(int userId)
        {
            return GameLogic.IsGameOver(GetByUserId(userId));
        }

        public void Remove(int gameId)
        {
            _repository.Remove(gameId);
        }
    }
}
