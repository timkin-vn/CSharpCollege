using System.Web.Http;
using Game2048.Common;
using Game2048.Common.Dto;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;

namespace Game2048.WebApi.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        [HttpGet, Route("get-by-user-id/{id}")]
        public GameReply GetByUserId(int id)
        {
            return ToReply(_service.GetByUserId(id));
        }

        [HttpPost, Route("make-move")]
        public GameReply MakeMove([FromBody] MakeMoveRequest request)
        {
            var game = _service.MakeMove(request.UserId, (MoveDirection)request.Direction);
            return ToReply(game);
        }

        [HttpGet, Route("is-over/{id}")]
        public BooleanReply IsOver(int id)
        {
            return new BooleanReply { Value = _service.IsGameOver(id) };
        }

        [HttpGet, Route("is-won/{id}")]
        public BooleanReply IsWon(int id)
        {
            return new BooleanReply { Value = _service.IsWon(id) };
        }

        [HttpDelete, Route("remove/{id}")]
        public void Remove(int id)
        {
            _service.Remove(id);
        }

        private static GameReply ToReply(GameModel game)
        {
            int n = Constants.Size;
            var cells = new int[n * n];
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    cells[r * n + c] = game.Field[r, c];

            return new GameReply
            {
                Id = game.Id,
                UserId = game.UserId,
                Score = game.Score,
                MoveCount = game.MoveCount,
                Cells = cells,
                Size = n
            };
        }
    }
}
