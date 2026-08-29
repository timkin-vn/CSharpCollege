using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessDtos;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private readonly ServerGameService _service = new ServerGameService();

        [HttpPost, Route("create")]
        public IHttpActionResult Create(CreateGameRequest req)
        {
            return Ok(_service.CreateGame(req.UserId, req.Mode));
        }

        [HttpPost, Route("move")]
        public IHttpActionResult MakeMove(MoveRequest req)
        {
            return Ok(_service.MakeMove(req.GameId, req.Row, req.Column));
        }

        [HttpGet, Route("get/{id}")]
        public IHttpActionResult Get(int id) => Ok(_service.GetGame(id));
    }
}