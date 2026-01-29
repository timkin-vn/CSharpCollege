using FifteenGame.Business;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    public class GameController : ApiController
    {
        private readonly Game2048Service _gameService;
        private readonly IGameRepository _gameRepository;

        public GameController()
        {
            _gameService = new Game2048Service();
            _gameRepository = NinjectKernel.Instance.Get<IGameRepository>();
        }

        [HttpPost]
        [Route("api/game/new")]
        public GameDto NewGame([FromBody] NewGameRequest request)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"NewGame called for UserId: {request.UserId}");
                
                var gameDto = _gameService.InitializeGame();
                gameDto.UserId = request.UserId;
                
                System.Diagnostics.Debug.WriteLine($"Game initialized with score: {gameDto.Score}");
                
                // Save to database
                var gameId = _gameRepository.Save(gameDto);
                gameDto.Id = gameId;
                
                System.Diagnostics.Debug.WriteLine($"Game saved with ID: {gameId}");
                
                return gameDto;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in NewGame: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("api/game/move")]
        public GameDto MakeMove([FromBody] MoveRequest request)
        {
            var gameDto = _gameRepository.GetByGameId(request.GameId);
            if (gameDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            gameDto = _gameService.MakeMove(gameDto, request.Direction);
            _gameRepository.Save(gameDto);
            
            return gameDto;
        }

        [HttpPost]
        [Route("api/game/get-by-user-id")]
        public IEnumerable<GameDto> GetByUserId([FromBody] UserIdRequest request)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetByUserId called for UserId: {request.UserId}");
                
                var games = _gameRepository.GetByUserId(request.UserId);
                
                System.Diagnostics.Debug.WriteLine($"Found {games?.Count()} games for user {request.UserId}");
                
                return games;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetByUserId: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("api/game/get-by-game-id")]
        public GameDto GetByGameId([FromBody] GameIdRequest request)
        {
            var gameDto = _gameRepository.GetByGameId(request.GameId);
            if (gameDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return gameDto;
        }

        [HttpDelete]
        [Route("api/game/remove")]
        public void Remove([FromBody] GameIdRequest request)
        {
            _gameRepository.Remove(request.GameId);
        }
    }
}
