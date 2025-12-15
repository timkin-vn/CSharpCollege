using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController()
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

        [HttpPost]
        [Route("api/game/make-move")]
        public GameReply MakeMove([FromBody] MakeMoveRequest request)
        {
            var gameModel = _gameService.MakeMove(
                request.GameId,
                request.Row,
                request.Column
            );

            return ToDto(gameModel);
        }

        [HttpPost]
        [Route("api/game/is-over")]
        public BooleanReply IsOver([FromBody] GameIdRequest request)
        {
            return new BooleanReply
            {
                IsTrue = _gameService.IsGameOver(request.GameId)
            };
        }

        [HttpDelete]
        [Route("api/game/remove")]
        public void Remove([FromBody] GameIdRequest request)
        {
            _gameService.RemoveGame(request.GameId);
        }

        private GameReply ToDto(GameModel model)
        {
            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                Cells = new int[GameModel.RowCount * GameModel.ColumnCount]
            };

            int index = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    dto.Cells[index++] = model[row, column] ? 1 : 0;
                }
            }

            return dto;
        }
    }
}
