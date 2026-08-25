using System.Web.Http;
using TicTacToeGame.Common; // Убедитесь, что эта ссылка добавлена
using TicTacToeGame.Business; // Убедитесь, что эта ссылка добавлена

namespace TicTacToeClient.WebApi.Controllers // Проверьте пространство имен
{
    public class GameController : ApiController
    {
        private static GameState _game = new GameState();
        private GameService _service = new GameService();

        [HttpPost]
        [Route("api/game/move/{pos}")]
        public IHttpActionResult MakeMove(int pos)
        {
            _game = _service.MakeMove(_game, pos);
            return Ok(_game);
        }
    }
}