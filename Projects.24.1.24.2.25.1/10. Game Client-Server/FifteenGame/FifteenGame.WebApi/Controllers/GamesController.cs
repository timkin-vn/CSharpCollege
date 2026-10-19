using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Linq;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService =
            NinjectKernel.Instance.Get<IGameService>();

        [HttpGet]
        [Route("api/games/get-by-game-id/{gameId}")]
        public IHttpActionResult GetByGameId(int gameId)
        {
            if (gameId <= 0)
                return BadRequest("Идентификатор партии должен быть положительным.");

            var game = _gameService.GetByGameId(gameId);

            if (game == null)
                return NotFound();

            return Ok(ToDto(game));
        }

        [HttpGet]
        [Route("api/games/get-by-user-id/{userId}")]
        public IHttpActionResult GetByUserId(int userId)
        {
            if (userId <= 0)
                return BadRequest("Идентификатор пользователя должен быть положительным.");

            var game = _gameService.GetByUserId(userId);

            if (game == null)
                return NotFound();

            return Ok(ToDto(game));
        }

        [HttpGet]
        [Route("api/games/is-over/{gameId}")]
        public IHttpActionResult IsGameOver(int gameId)
        {
            if (gameId <= 0)
                return BadRequest("Некорректный идентификатор партии.");

            var game = _gameService.GetByGameId(gameId);

            if (game == null)
                return NotFound();

            return Ok(new BooleanReply
            {
                IsTrue = _gameService.IsGameOver(gameId)
            });
        }

        [HttpPost]
        [Route("api/games/make-move")]
        public IHttpActionResult MakeMove(
            [FromBody] MakeMoveRequest request)
        {
            if (request == null)
                return BadRequest("Тело запроса отсутствует.");

            if (request.GameId <= 0)
                return BadRequest("Некорректный идентификатор партии.");

            if (string.IsNullOrWhiteSpace(request.Direction))
                return BadRequest("Не указано направление хода.");

            MoveDirection direction;

            if (!Enum.TryParse(request.Direction, true, out direction) ||
                !Enum.IsDefined(typeof(MoveDirection), direction) ||
                direction == MoveDirection.None)
            {
                return BadRequest("Указано неизвестное направление хода.");
            }

            var game = _gameService.GetByGameId(request.GameId);

            if (game == null)
                return NotFound();

            var updatedGame = _gameService.MakeMove(
                request.GameId, direction);

            if (updatedGame == null)
                return BadRequest("Не удалось выполнить ход.");

            return Ok(ToDto(updatedGame));
        }

        [HttpDelete]
        [Route("api/games/remove/{gameId}")]
        public IHttpActionResult RemoveGame(int gameId)
        {
            if (gameId <= 0)
                return BadRequest("Некорректный идентификатор партии.");

            if (_gameService.GetByGameId(gameId) == null)
                return NotFound();

            _gameService.RemoveGame(gameId);

            return Ok();
        }

        private GameReply ToDto(GameModel model)
        {
            if (model == null)
                return null;

            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                FreeCellColumn = model.FreeCellColumn,
                FreeCellRow = model.FreeCellRow,
                Cells = new int[
                    Constants.RowCount * Constants.ColumnCount]
            };

            int index = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    dto.Cells[index++] = model[row, column];
                }
            }

            return dto;
        }
    }
}