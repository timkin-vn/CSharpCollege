using Ninject;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Definitions;
using Nonogram.Common.Infrastructure;
using Nonogram.Common.Services;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Nonogram.Server.Controllers
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
            var gameModel = _gameService.MakeMove(request.GameId, request.Row, request.Column);
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

        [HttpPost]
        [Route("api/game/is-won")]
        public BooleanReply IsWon([FromBody] GameIdRequest request)
        {
            return new BooleanReply
            {
                IsTrue = _gameService.IsGameWon(request.GameId)
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
            if (model == null) return null;

            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MistakesCount = model.MistakesCount,
                RowClues = model.RowClues,
                ColumnClues = model.ColumnClues,
                Cells = new int[Constants.RowCount * Constants.ColumnCount]
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