using _2048Game.Business.Services;
using _2048Game.Common.BusinessDtos;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Definitions;
using _2048Game.Common.Services;
using System;
using System.Web.Http;

namespace _2048Game.Server.Controllers
{
    /// <summary>
    /// Контроллер для игр
    /// </summary>
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;
        /// <summary>
        /// Контроллер
        /// </summary>     
        public GameController()
        {
            _gameService = new GameService();
        }
        /// <summary>
        /// Контроллер с параметром
        /// </summary>
        /// <param name="gameService"></param>
        public GameController(IGameService gameService)
        {
            _gameService = gameService;  
        }

        /// <summary>
        /// Получение модели игры по ID пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/get-by-userId")]
        public GameReply GetByUserId([FromBody] UserIdRequest request)
        {
            var gameModel = _gameService.GetByUserId(request.UserId);
            return ToDto(gameModel);
        }
        /// <summary>
        /// Получение модели игры по ее ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/get-by-gameId")]
        public GameReply GetByGameId([FromBody] GameIdRequest request)
        {
            var gameModel = _gameService.GetByGameId(request.GameId);
            return ToDto(gameModel);
        }

        /// <summary>
        /// Удаление игры
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/remove-game")]
        public IHttpActionResult RemoveGame([FromBody] GameIdRequest request)
        {
            _gameService.RemoveGame(request.GameId);
            return Ok();
        }

        /// <summary>
        /// Проверка, одержал ли пользователь победу
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/game/check-game-over/{gameId}")]
        public bool CheckGameOver(int gameId)
        {
            return _gameService.CheckGameOver(gameId);
        }
        /// <summary>
        /// Сохранение
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/save")]
        public GameReply Save([FromBody] UserIdRequest request)
        {
            int id = _gameService.Save();
            var board = _gameService.Board;
            board.Id = id;
            return ToDto(board);
        }
        /// <summary>
        /// Начало игры
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/start-game")]
        public IHttpActionResult StartGame([FromBody] UserIdRequest request)
        {
            var existingGame = _gameService.GetByUserId(request.UserId);
            if (existingGame == null)
            {
                _gameService.StartGame(request.UserId);
            }
            var board = _gameService.Board;
            if (board == null)
            {
                return InternalServerError(new Exception("Не удалось создать игру"));
            }
            return Ok(ToDto(board));
        }
        /// <summary>
        /// Перезапуск
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/restart")]
        public IHttpActionResult Restart([FromBody] UserIdRequest request)
        {
            _gameService.Restart(request.UserId);
            
            var board = _gameService.Board;
            return Ok(ToDto(board));
        }
        /// <summary>
        /// Движение
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/game/move")]
        public MoveReply Move([FromBody] MoveRequest request)
        {
            if (_gameService.Board == null || _gameService.Board.UserId != request.UserId)
            {
                _gameService.StartGame(request.UserId);
            }
            bool success = _gameService.Move(request.direction);

            return new MoveReply
            {
                Success = success,
                Tiles = _gameService.Board.Tiles,
                GameOver = !_gameService.Board.CanMove(),
                MoveCount = _gameService.Board.MoveCount
            };
        }

        private GameReply ToDto(GameModel model)
        {
            if (model == null)
            {
                return null;
            }
            var dto = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                Tiles = new int[Constants.RowCount, Constants.ColumnCount]
            };

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    dto.Tiles[r, c] = model.Tiles[r, c];
                }
            }
            return dto;
        }
    }
}
