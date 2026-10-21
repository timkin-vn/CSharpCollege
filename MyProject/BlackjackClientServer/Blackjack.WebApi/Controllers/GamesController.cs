using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blackjack.WebApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET api/games/current?userId=1
        [HttpGet("current")]
        public ActionResult<GameModel> GetCurrent([FromQuery] int userId)
        {
            return Ok(_gameService.GetCurrentGame(userId));
        }

        // POST api/games/new?userId=1
        [HttpPost("new")]
        public ActionResult<GameModel> StartNew([FromQuery] int userId)
        {
            return Ok(_gameService.StartNewGame(userId));
        }

        // POST api/games/5/hit
        [HttpPost("{id}/hit")]
        public ActionResult<GameModel> Hit(int id)
        {
            try
            {
                return Ok(_gameService.Hit(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/games/5/stand
        [HttpPost("{id}/stand")]
        public ActionResult<GameModel> Stand(int id)
        {
            try
            {
                return Ok(_gameService.Stand(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/games/stats?userId=1
        [HttpGet("stats")]
        public ActionResult<UserStatsModel> GetStats([FromQuery] int userId)
        {
            return Ok(_gameService.GetStats(userId));
        }

        // GET api/games/history?userId=1
        [HttpGet("history")]
        public ActionResult<List<GameHistoryItem>> GetHistory([FromQuery] int userId)
        {
            return Ok(_gameService.GetHistory(userId));
        }
    }
}
