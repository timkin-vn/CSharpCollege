using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Core.BusinessLogic.Models;
using Ninject;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    /// <summary>Контроллер игры «Ключи и монстры»</summary>
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();

        /// <summary>Текущая игра пользователя (если нет или закончена — создаётся новая)</summary>
        [HttpGet]
        [Route("api/games/get-by-user-id/{userId}")]
        public GameSnapshot GetByUserId(int userId)
        {
            return _gameService.GetByUserId(userId);
        }

        /// <summary>Начать новую игру</summary>
        [HttpPost]
        [Route("api/games/new/{userId}")]
        public GameSnapshot NewGame(int userId)
        {
            return _gameService.NewGame(userId);
        }

        /// <summary>Сделать ход</summary>
        [HttpPost]
        [Route("api/games/move")]
        public GameSnapshot MakeMove([FromBody] MakeMoveRequest request)
        {
            return _gameService.MakeMove(request.UserId, request.RowDelta, request.ColumnDelta);
        }
    }
}
