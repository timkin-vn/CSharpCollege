using System.Web.Http;
using Minesweeper.Common;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.WebApi.Controllers
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

        [HttpPost, Route("apply")]
        public GameReply Apply([FromBody] CellActionRequest request)
        {
            var game = _service.Apply(request.UserId, request.Row, request.Col, (CellAction)request.Action);
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
                    cells[r * n + c] = Encode(game.Field[r, c]);

            return new GameReply
            {
                Id = game.Id,
                UserId = game.UserId,
                IsLost = game.IsLost,
                MoveCount = game.MoveCount,
                MinesRemaining = GameLogic.MinesRemaining(game),
                Cells = cells,
                Size = n
            };
        }

        private static int Encode(Cell cell)
        {
            if (cell.IsRevealed)
                return cell.IsMine ? -1 : cell.AdjacentMines;
            if (cell.IsFlagged) return -2;
            return -3;
        }
    }
}
