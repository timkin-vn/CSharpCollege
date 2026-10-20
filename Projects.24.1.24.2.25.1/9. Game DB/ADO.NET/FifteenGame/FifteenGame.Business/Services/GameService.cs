using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Core.BusinessLogic.Models;
using Newtonsoft.Json;
using CoreGameService = FifteenGame.Core.BusinessLogic.Services.GameService;

namespace FifteenGame.Business.Services
{
    /// <summary>Хранит состояние игры каждого пользователя в БД (в виде JSON-снимка).</summary>
    public class GameService : IGameService
    {
        private readonly IMazeGameRepository _repository;

        public GameService(IMazeGameRepository repository)
        {
            _repository = repository;
        }

        public GameSnapshot GetByUserId(int userId)
        {
            var game = Restore(_repository.GetByUserId(userId));
            if (game == null || game.State != GameState.Playing)
            {
                return NewGame(userId);
            }

            return game.ToSnapshot();
        }

        public GameSnapshot NewGame(int userId)
        {
            return Save(userId, new CoreGameService());
        }

        public GameSnapshot MakeMove(int userId, int dRow, int dColumn)
        {
            var model = _repository.GetByUserId(userId);
            var game = Restore(model);
            if (game == null)
            {
                return NewGame(userId);
            }

            game.TryMove(dRow, dColumn);
            return Save(userId, game, model);
        }

        private GameSnapshot Save(int userId, CoreGameService game, MazeGameModel model = null)
        {
            var snapshot = game.ToSnapshot();

            model = model ?? _repository.GetByUserId(userId) ?? new MazeGameModel { UserId = userId };
            model.SerializedGameManager = JsonConvert.SerializeObject(snapshot);
            _repository.Save(model);

            return snapshot;
        }

        private static CoreGameService Restore(MazeGameModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.SerializedGameManager))
            {
                return null;
            }

            try
            {
                var snapshot = JsonConvert.DeserializeObject<GameSnapshot>(model.SerializedGameManager);
                var game = new CoreGameService();
                return game.TryRestore(snapshot) ? game : null;
            }
            catch (JsonException)
            {
                // Сохранение от старой версии игры — просто начнём новую.
                return null;
            }
        }
    }
}
