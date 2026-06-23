using Minesweeper.Common;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.Business
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

        public GameModel Apply(int userId, int row, int col, CellAction action)
        {
            var game = GetByUserId(userId);
            if (!GameLogic.IsWon(game) && !GameLogic.IsGameOver(game))
            {
                if (GameLogic.Apply(game, row, col, action))
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
