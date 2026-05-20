using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController()
        {
            _gameService = NinjectKernel.Instance.Get<IGameService>();
        }

        [HttpPost]
        [Route("api/game/get-by-user-id")]
        public GameReply GetByUserId([FromBody] UserIdRequest request)
        {
            var gameModel = _gameService.GetByUserId(request.UserId);
            return ToDto(gameModel);
        }

        [HttpPost]
        [Route("api/game/get-by-game-id")]
        public GameReply GetByGameId([FromBody] GameIdRequest request)
        {
            var gameModel = _gameService.GetByGameId(request.GameId);
            return ToDto(gameModel);
        }

        [HttpDelete]
        [Route("api/game/remove")]
        public void Remove([FromBody] GameIdRequest request)
        {
            _gameService.RemoveGame(request.GameId);
        }

        [HttpPost]
        [Route("api/game/save")]
        public void Save([FromBody] GameReply request)
        {
            var model = FromDto(request);
            _gameService.Save(model);
        }

        private GameModel FromDto(GameReply reply)
        {
            var model = new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                MatchesCount = reply.MatchesCount,
                IsFinished = reply.IsFinished,
            };
            int i = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    model[row, col] = reply.Cells[i++];
            return model;
        }

        private GameReply ToDto(GameModel model)
        {
            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MatchesCount = model.MatchesCount,
                IsFinished = model.IsFinished,
                Cells = new int[GameModel.RowCount * GameModel.ColumnCount],
            };

            int i = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    dto.Cells[i++] = model[row, col];

            return dto;
        }
    }
}