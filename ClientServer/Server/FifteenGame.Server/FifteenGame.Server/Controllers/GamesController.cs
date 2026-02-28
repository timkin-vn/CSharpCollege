using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using System.Web.Http; 

namespace FifteenGame.Server.Controllers
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        [Route("api/game/start")]
        public StartGameReply Start(int userId) 
        {
            return _gameService.StartNewGame(userId);
        }

        [HttpPost]
        [Route("api/game/shoot")]
        public GameReply Shoot([FromBody] MakeMoveRequest request)
        {
            
            return _gameService.MakeMove(request);
        }
    }
}
