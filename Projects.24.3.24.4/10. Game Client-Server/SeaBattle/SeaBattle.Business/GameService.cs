using System.Collections.Generic;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;

namespace SeaBattle.Business
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel SaveResult(int userId, int moveCount, bool won)
        {
            var game = new GameModel { UserId = userId, MoveCount = moveCount, Won = won };
            return _repository.Save(game);
        }

        public IList<GameModel> GetHistory(int userId)
        {
            return _repository.GetByUserId(userId);
        }
    }
}
