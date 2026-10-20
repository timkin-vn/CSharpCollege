using FifteenGame.Common.Contracts;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Models;
using Ninject;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
	public class GamesController : ApiController
	{
		private readonly IGameService _gameService = FifteenGame.Common.Infrastructure.NinjectKernel.Instance.Get<IGameService>();

		[HttpGet]
		[Route("api/games/get-by-user-id/{userId}")]
		public GameReply GetOrCreateGame(int userId)
		{
			var model = _gameService.GetOrCreateGame(userId);
			return ToDto(model);
		}

		[HttpPost]
		[Route("api/games/make-move")]
		public GameReply MakeMove([FromBody] MakeMoveRequest request)
		{
			var model = _gameService.MakeMove(request.GameId, request.Row, request.Column);
			return ToDto(model);
		}

		private GameReply ToDto(GameModel model)
		{
			if (model == null) return null;
			return new GameReply
			{
				Id = model.Id,
				UserId = model.UserId,
				BoardState = model.BoardState,
				Result = (int)model.Result,
				PlayerScore = model.PlayerScore,
				ComputerScore = model.ComputerScore
			};
		}
	}
}