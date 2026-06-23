using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SeaBattle.Common.Dto;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;

namespace SeaBattle.WebApi.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        [HttpPost, Route("save")]
        public GameReply Save([FromBody] SaveGameRequest request)
        {
            var game = _service.SaveResult(request.UserId, request.MoveCount, request.Won);
            return ToReply(game);
        }

        [HttpGet, Route("history/{id}")]
        public IList<GameReply> History(int id)
        {
            return _service.GetHistory(id).Select(ToReply).ToList();
        }

        private static GameReply ToReply(GameModel game)
        {
            return new GameReply
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                Won = game.Won
            };
        }
    }
}
