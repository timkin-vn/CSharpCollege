using System;
using System.Threading.Tasks;
using System.Web.Http;
using MinesweeperGame.Business.Services;
using MinesweeperGame.Common.Contracts;
using MinesweeperGame.Common.Definitions;
using MinesweeperGame.Common.Dtos;
using MinesweeperGame.DataAccess.EF.Repositories;

namespace MinesweeperGame.WebApi.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController()
        {
            _gameService = new GameService(new GameRepository());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateGame([FromBody] CreateGameRequest request)
        {
            if (request == null) return BadRequest("Запрос не может быть пустым");
            var result = await _gameService.CreateGameAsync(request.Difficulty);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet]
        [Route("{gameId:guid}")]
        public async Task<IHttpActionResult> GetGame(Guid gameId)
        {
            var result = await _gameService.GetGameAsync(gameId);
            if (!result.Success) return NotFound();
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("{gameId:guid}/reveal")]
        public async Task<IHttpActionResult> RevealCell(Guid gameId, [FromBody] RevealCellRequest request)
        {
            if (request == null) return BadRequest("Запрос не может быть пустым");
            var result = await _gameService.RevealCellAsync(gameId, request.Row, request.Column);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("{gameId:guid}/flag")]
        public async Task<IHttpActionResult> ToggleFlag(Guid gameId, [FromBody] ToggleFlagRequest request)
        {
            if (request == null) return BadRequest("Запрос не может быть пустым");
            var result = await _gameService.ToggleFlagAsync(gameId, request.Row, request.Column);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }
    }
}
