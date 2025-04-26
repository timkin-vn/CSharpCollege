using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController()
        {
            _gameService = NinjectKernel.Instance.Get<IGameService>();
        }

        [HttpPost]
        [Route("api/games/get-by-user-id")]
        public GameReply GetByUserId([FromBody] GetByUserIdRequest request)
        {
            var reply = _gameService.GetByUserId(request.UserId);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/games/get-by-id")]
        public GameReply GetByGameId([FromBody] GetByGameIdRequest request)
        {
            var reply = _gameService.GetByGameId(request.GameId);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/games/make-move")]
        public GameReply MakeMove([FromBody] MakeMoveRequest request)
        {
            var fisrsbuutonRowCol =  request.OnebuutonRowCol; 
            var secondbuutonRowCol =  request.TwobuutonRowCol;
            var reply = _gameService.CheckMatch(request.GameId, fisrsbuutonRowCol,secondbuutonRowCol);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/games/is-game-over")]
        public BooleanReply IsGameOver([FromBody] GetByGameIdRequest request)
        {
            var reply = _gameService.IsGameOver(request.GameId);
            return new BooleanReply { Value = reply, };
        }

        [HttpDelete]
        [Route("api/games/remove")]
        public void Remove([FromBody] GetByGameIdRequest request)
        {
            _gameService.RemoveGame(request.GameId);
        }

        private GameReply ToDto(GameModel game)
        {
            if (game == null)
            {
                return null;
            }

            var dto = new GameReply
            {
                GameId = game.GameId,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                GameStart = game.GameStart,
                Cells = new List<string>(),
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells.Add(game.Field[row, column].ToString());
                }
            }

            return dto;
        }
    }
}
