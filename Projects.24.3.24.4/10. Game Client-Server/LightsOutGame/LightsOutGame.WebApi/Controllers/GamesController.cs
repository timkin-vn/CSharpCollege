using LightsOutGame.Common.BusinessDtos;
using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Infrastucture;
using Ninject;
using System.Web.Http;

namespace LightsOutGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();

        [HttpGet]
        [Route("api/games/get-by-user-id/{userId}")]
        public GameReply GetByUserId(int userId)
        {
            return ToDto(_gameService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("api/games/get-by-game-id/{gameId}")]
        public GameReply GetByGameId(int gameId)
        {
            return ToDto(_gameService.GetByGameId(gameId));
        }

        [HttpPost]
        [Route("api/games/make-move")]
        public GameReply MakeMove([FromBody] MakeMoveRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return ToDto(_gameService.MakeMove(request.GameId, request.Row, request.Column));
        }

        [HttpGet]
        [Route("api/games/is-over/{gameId}")]
        public BooleanReply IsOver(int gameId)
        {
            return new BooleanReply
            {
                IsTrue = _gameService.IsGameOver(gameId),
            };
        }

        [HttpDelete]
        [Route("api/games/remove/{gameId}")]
        public void Remove(int gameId)
        {
            _gameService.RemoveGame(gameId);
        }

        private GameReply ToDto(GameModel model)
        {
            if (model == null)
            {
                return null;
            }

            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                Cells = new int[Constants.RowCount * Constants.ColumnCount],
            };

            int cellIndex = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[cellIndex++] = model[row, column];
                }
            }

            return dto;
        }
    }
}
