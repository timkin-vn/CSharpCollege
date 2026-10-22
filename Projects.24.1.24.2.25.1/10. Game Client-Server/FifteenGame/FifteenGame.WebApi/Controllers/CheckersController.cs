using CheckersGame.Business.Contracts;
using CheckersGame.Business.Models;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Linq;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    /// <summary>
    /// Контроллер игры «Шашки»
    /// </summary>
    public class CheckersController : ApiController
    {
        private readonly ICheckersGameService _gameService = NinjectKernel.Instance.Get<ICheckersGameService>();

        [HttpPost]
        [Route("api/checkers/create/{userId}")]
        public CheckersGameReply CreateNewGame(int userId)
        {
            return ToDto(_gameService.CreateNewGame(userId));
        }

        [HttpGet]
        [Route("api/checkers/last-active/{userId}")]
        public CheckersGameReply GetLastActiveGame(int userId)
        {
            return ToDto(_gameService.GetLastActiveGame(userId));
        }

        [HttpGet]
        [Route("api/checkers/get/{gameId}")]
        public IHttpActionResult LoadGame(int gameId)
        {
            try
            {
                return Ok(ToDto(_gameService.LoadGame(gameId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/checkers/make-move")]
        public IHttpActionResult MakeMove([FromBody] CheckersMakeMoveRequest request)
        {
            try
            {
                _gameService.MakeMove(request.GameId, request.FromRow, request.FromCol, request.ToRow, request.ToCol);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/checkers/finish-capture/{gameId}")]
        public IHttpActionResult FinishCapture(int gameId)
        {
            try
            {
                _gameService.FinishCapture(gameId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/checkers/is-over/{gameId}")]
        public BooleanReply IsGameOver(int gameId)
        {
            return new BooleanReply
            {
                IsTrue = _gameService.IsGameOver(gameId),
            };
        }

        [HttpDelete]
        [Route("api/checkers/remove/{gameId}")]
        public void DeleteGame(int gameId)
        {
            _gameService.DeleteGame(gameId);
        }

        private static CheckersGameReply ToDto(GameModel model)
        {
            if (model == null)
            {
                return null;
            }

            var dto = new CheckersGameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                CurrentPlayer = (int)model.CurrentPlayer,
                IsCaptureInProgress = model.IsCaptureInProgress,
                CaptureRow = model.CaptureRow,
                CaptureCol = model.CaptureCol,
                IsGameOver = model.IsGameOver(),
                Cells = new int[GameModel.BoardSize * GameModel.BoardSize],
                CapturedCells = model.CapturedThisTurn.Select(c => c.Item1 * GameModel.BoardSize + c.Item2).ToArray(),
            };

            int index = 0;
            for (int row = 0; row < GameModel.BoardSize; row++)
            {
                for (int col = 0; col < GameModel.BoardSize; col++)
                {
                    dto.Cells[index++] = (int)model[row, col];
                }
            }

            return dto;
        }
    }
}
