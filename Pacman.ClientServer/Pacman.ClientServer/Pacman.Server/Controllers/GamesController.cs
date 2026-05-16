using System;
using System.Web.Http;
using Pacman.Common.Interfaces.Services;
using Pacman.Common.Enums;

namespace Pacman.Server.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateGame(int userId)
        {
            try
            {
                var gameState = _gameService.CreateNewGame(userId);
                return Ok(gameState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetGame(int id)
        {
            try
            {
                var gameState = _gameService.GetGameState(id);
                if (gameState == null)
                    return NotFound();
                return Ok(gameState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [HttpPut]
        [Route("{id}/move")]
        public IHttpActionResult Move(int id, Direction direction)
        {
            try
            {
                var gameState = _gameService.Move(id, direction);
                return Ok(gameState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteGame(int id)
        {
            try
            {
                _gameService.RemoveGame(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet]
        [Route("user/{userId}")]
        public IHttpActionResult GetUserGames(int userId)
        {
            try
            {
                var games = _gameService.GetUserGames(userId);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
