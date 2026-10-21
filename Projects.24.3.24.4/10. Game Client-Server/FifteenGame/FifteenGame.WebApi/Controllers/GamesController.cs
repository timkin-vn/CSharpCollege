using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
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

        [HttpGet]
        [Route("api/games/start-new/{userId}/{difficulty}/{theme}")]

        public GameReply StartNewGame(int userId, int difficulty, int theme)
        {
            return ToDto(_gameService.StartNewGame(userId, difficulty, theme));
        }

        [HttpPost]
        [Route("api/games/make-move")]

        public GameReply MakeMove([FromBody] MakeMoveRequest request)
        {
            if (request == null)
            {
                return null;
            }

            if (request.UseHint || string.Equals(request.Direction, "hint", StringComparison.OrdinalIgnoreCase))
            {
                return ToDto(_gameService.UseHint(request.GameId, request.ElapsedSeconds));
            }

            if (request.Row >= 0 && request.Column >= 0)
            {
                return ToDto(_gameService.OpenCard(request.GameId, request.Row, request.Column, request.ElapsedSeconds));
            }

            if (!string.IsNullOrWhiteSpace(request.Direction) && request.Direction.Contains(","))
            {
                var parts = request.Direction.Split(',');

                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int r) && int.TryParse(parts[1].Trim(), out int c))
                {
                    return ToDto(_gameService.OpenCard(request.GameId, r, c, request.ElapsedSeconds));
                }
            }

            if (Enum.TryParse<MoveDirection> (request.Direction, out var moveDirection))
        {
                return ToDto(_gameService.MakeMove(request.GameId, moveDirection));
            }

            return ToDto(_gameService.GetByGameId(request.GameId));
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
                FreeCellColumn = model.FreeCellColumn,
                FreeCellRow = model.FreeCellRow,
                Difficulty = model.Difficulty,
                Theme = model.Theme,
                Score = model.Score,
                ComboStreak = model.ComboStreak,
                HintsLeft = model.HintsLeft,
                ElapsedSeconds = model.ElapsedSeconds,
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
