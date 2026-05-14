using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.Infrastructure;
using Minesweeper.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Minesweeper.Server.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok("Games controller is alive");
        }

        /// <summary>
        /// Создать новую игру
        /// </summary>
        [HttpPost]
        [Route("new")]
        public IHttpActionResult CreateGame([FromBody] CreateGameRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is required");

                if (request.Size < 5 || request.Size > 20)
                    return BadRequest("Field size must be between 5 and 20");

                if (request.MineCount < 1 || request.MineCount > request.Size * request.Size - 1)
                    return BadRequest($"Mine count must be between 1 and {request.Size * request.Size - 1}");

                var game = _gameService.CreateGame(request);
                return Ok(ApiResponse<GameResponse>.Ok(game, "Game created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить игру по ID
        /// </summary>
        [HttpGet]
        [Route("{gameId:int}")]
        public IHttpActionResult GetGame(int gameId)
        {
            try
            {
                var game = _gameService.GetGame(gameId);
                if (game == null)
                    return NotFound();

                return Ok(ApiResponse<GameResponse>.Ok(game, "Game found"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить последнюю активную игру пользователя
        /// </summary>
        [HttpGet]
        [Route("user/{userId}/active")]
        public IHttpActionResult GetLastActiveGame(int userId)
        {
            try
            {
                var game = _gameService.GetLastActiveGame(userId);
                if (game == null)
                    return NotFound();

                return Ok(ApiResponse<GameResponse>.Ok(game, "Active game found"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Открыть ячейку
        /// </summary>
        [HttpPost]
        [Route("{gameId}/open")]
        public IHttpActionResult OpenCell(int gameId, [FromBody] CellPositionRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is required");

                var makeMoveRequest = new MakeMoveRequest
                {
                    GameId = gameId,
                    Row = request.Row,
                    Column = request.Column,
                    Action = "open"
                };

                var game = _gameService.MakeMove(makeMoveRequest);
                return Ok(ApiResponse<GameResponse>.Ok(game, "Cell opened successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Переключить флаг
        /// </summary>
        [HttpPost]
        [Route("{gameId}/toggle-flag")]
        public IHttpActionResult ToggleFlag(int gameId, [FromBody] CellPositionRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is required");

                var makeMoveRequest = new MakeMoveRequest
                {
                    GameId = gameId,
                    Row = request.Row,
                    Column = request.Column,
                    Action = "toggle_flag"
                };

                var game = _gameService.MakeMove(makeMoveRequest);
                return Ok(ApiResponse<GameResponse>.Ok(game, "Flag toggled successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Сделать ход 
        /// </summary>
        [HttpPost]
        [Route("{gameId}/move")]
        public IHttpActionResult MakeMove(int gameId, [FromBody] MakeMoveRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Request is required");

                request.GameId = gameId;

                var game = _gameService.MakeMove(request);
                return Ok(ApiResponse<GameResponse>.Ok(game, "Move made successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<GameResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить игры пользователя
        /// </summary>
        [HttpGet]
        [Route("user/{userId}/all")]
        public IHttpActionResult GetUserGames(int userId)
        {
            try
            {
                var games = _gameService.GetUserGames(userId);
                return Ok(ApiResponse<IEnumerable<GameResponse>>.Ok(games, "User games retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IEnumerable<GameResponse>>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Проверить, окончена ли игра
        /// </summary>
        [HttpGet]
        [Route("{gameId}/is-over")]
        public IHttpActionResult IsGameOver(int gameId)
        {
            try
            {
                var isOver = _gameService.IsGameOver(gameId);
                return Ok(ApiResponse<bool>.Ok(isOver, "Game status retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Проверить, выиграна ли игра
        /// </summary>
        [HttpGet]
        [Route("{gameId}/is-won")]
        public IHttpActionResult IsGameWon(int gameId)
        {
            try
            {
                var isWon = _gameService.IsGameWon(gameId);
                return Ok(ApiResponse<bool>.Ok(isWon, "Game status retrieved"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Удалить игру
        /// </summary>
        [HttpDelete]
        [Route("{gameId:int}")]
        public IHttpActionResult DeleteGame(int gameId)
        {
            try
            {
                _gameService.DeleteGame(gameId);
                return Ok(ApiResponse<string>.Ok("Game deleted", "Game deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Error(ex.Message).Message);
            }
        }
    }

    /// <summary>
    /// Запрос для позиции ячейки
    /// </summary>
    public class CellPositionRequest
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}