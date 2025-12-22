using Pacman.Common.Dtos;
using Pacman.Common.Services;
using System.Web.Http;

namespace Pacman.Server.Controllers
{
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Route("api/game/level/{id}")]
        public MapDto GetLevel(int id)
        {
            return _gameService.GetLevel(id);
        }
    }
}