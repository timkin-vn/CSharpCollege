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
            if (!Enum.TryParse<MoveDirection>(request.Direction, out var moveDirection))
            {
                return null;
            }

            var gameModel = _gameService.MakeMove(request.GameId, moveDirection);
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
                FreeCellColumn = model.FreeCellColumn,
                FreeCellRow = model.FreeCellRow,
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
