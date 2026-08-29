using System;
using System.Web.Http;
using Pacman.Common.Interfaces.Services;

namespace Pacman.Server.Controllers
{
    [RoutePrefix("api/leaderboard")]
    public class LeaderboardController : ApiController
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetTopScores(int count = 10)
        {
            try
            {
                var topScores = _leaderboardService.GetTopScores(count);
                return Ok(topScores);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
