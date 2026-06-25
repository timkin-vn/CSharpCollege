using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FifteenGame.Core.BusinessLogic.Models;

namespace FifteenGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();
        private readonly IMazeGameService _mazeGameService = NinjectKernel.Instance.Get<IMazeGameService>();

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
            if (!Enum.TryParse<MoveDirection>(request.Direction, out var moveDirection))
            {
                return null;
            }

            return ToDto(_gameService.MakeMove(request.GameId, moveDirection));
        }

        [HttpDelete]
        [Route("api/games/remove/{gameId}")]
        public void RemoveGame(int gameId)
        {
            _gameService.RemoveGame(gameId);
        }

        [HttpPost]
        [Route("api/games/maze/start/{userId}")]
        public void StartMazeGame(int userId)
        {
            _mazeGameService.StartNewMazeGame(userId);
        }

        [HttpGet]
        [Route("api/games/maze/state/{userId}")]
        public MazeGameReply GetMazeGameState(int userId)
        {
            var level = _mazeGameService.GetMazeGameByUserId(userId);
            return ToMazeGameReply(level);
        }

        [HttpPost]
        [Route("api/games/maze/move")]
        public MazeGameReply MakeMazeMove([FromBody] MakeMazeMoveRequest request)
        {
            var level = _mazeGameService.MakeMazeMove(request.UserId, request.DeltaRow, request.DeltaCol);
            return ToMazeGameReply(level);
        }
                },
                Player = new PlayerDto
                {
                    Row = level.Player.Row,
                    Column = level.Player.Column,
                    Keys = level.Player.Keys,
                    Moves = level.Player.Moves
                }
            };

            for (int r = 0; r < Level.Rows; r++)
            {
                for (int c = 0; c < Level.Columns; c++)
                {
                    var cell = level.Grid[r, c];
                    if (cell != null)
                    {
                        reply.Level.Grid[r, c] = new CellDto
                        {
                            Row = r,
                            Column = c,
                            Type = cell.Type
                        };
                    }
                }
            }
            return reply;
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
