using MemoryGame.Common.BusinessDtos;
using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.Common.Definitions;
using MemoryGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MemoryGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();

        [HttpGet]
        [Route("api/games/get-by-game-id/{gameId}")]
        public GameReply GetByGameId(int gameId)
        {
            return ToDto(_gameService.GetByGameId(gameId));
        }

        [HttpGet]
        [Route("api/games/get-by-user-id/{userId}")]
        public GameReply GetByUserId(int userId)
        {
            return ToDto(_gameService.GetByUserId(userId));
        }

        [HttpGet]
        [Route("api/games/is-over/{gameId}")]
        public BooleanReply IsGameOver(int gameId)
        {
            return new BooleanReply
            {
                IsTrue = _gameService.IsGameOver(gameId),
            };
        }

        [HttpPost]
                [Route("api/games/make-move")]
                public GameReply MakeMove([FromBody] MakeMoveRequest request)
                {
                    return ToDto(_gameService.MakeMove(request.GameId, request.Row, request.Column));
                }

        [HttpDelete]
        [Route("api/games/remove/{gameId}")]
        public void RemoveGame(int gameId)
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
                                        States = new int[Constants.RowCount * Constants.ColumnCount],
                                    };

            int cellIndex = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                                dto.Cells[cellIndex] = model[row, column];
                                dto.States[cellIndex] = (int)model.GetState(row, column);
                                cellIndex++;
                            }
                        }

            return dto;
        }
    }
}
