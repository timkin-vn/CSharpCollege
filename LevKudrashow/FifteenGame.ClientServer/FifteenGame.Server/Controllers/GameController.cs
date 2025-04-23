using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public GameReply GetByUserId([FromBody] GetGameByUserIdRequest request)
        {
            var reply = _gameService.GetByUserId(request.UserId);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/game/get-by-game-id")]
        public GameReply GetByGameId([FromBody] GetGameByGameIdRequest request)
        {
            var reply = _gameService.GetByGameId(request.GameId);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/game/make-move")]
        public GameReply MakeMove([FromBody] GameMakeMoveRequest request)
        {
            Enum.TryParse<MoveDirection>(request.Direction, out var direction);
            var reply = _gameService.MakeMove(request.GameId, direction);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/game/is-game-over")]
        public BooleanReply IsGameOver([FromBody] GetGameByGameIdRequest request)
        {
            var reply = _gameService.IsGameOver(request.GameId);
            return new BooleanReply { Value = reply, };
        }

        [HttpDelete]
        [Route("api/game/remove-game")]
        public void RemoveGame([FromBody] GetGameByGameIdRequest request)
        {
            _gameService.RemoveGame(request.GameId);
        }

        private GameReply ToDto(GameModel game)
        {
            var reply = new GameReply
            {
                Id = game.Id,
                UserId = game.UserId,
                GameBegin = game.GameBegin,
                ScoreCount = game.ScoreCount,
                FreeCellColumn = game.FreeCellColumn,
                FreeCellRow = game.FreeCellRow,
                Cells = new List<int>(),
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    reply.Cells.Add(game[row, column]);
                }
            }

            return reply;
        }
    }
}