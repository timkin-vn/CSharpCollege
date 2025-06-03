using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/tictactoe")]
    public class TicTacToeApiController : ControllerBase
    {
        private readonly TicTacToeService _ticTacToeService;
        private readonly ILogger<TicTacToeApiController> _logger;

        public TicTacToeApiController(TicTacToeService ticTacToeService, ILogger<TicTacToeApiController> logger)
        {
            _ticTacToeService = ticTacToeService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<TicTacToeGame> GetGame()
        {
            try
            {
                _logger.LogInformation("Запрос текущего состояния игры");
                return _ticTacToeService.GetGame();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении состояния игры");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("move")]
        public ActionResult<TicTacToeGame> MakeMove([FromBody] MoveRequest move)
        {
            try
            {
                if (move == null)
                {
                    _logger.LogWarning("Получен пустой запрос хода");
                    return BadRequest("Данные хода отсутствуют");
                }

                _logger.LogInformation($"Попытка хода: строка {move.Row}, столбец {move.Col}");
                
                bool success = _ticTacToeService.MakeMove(move.Row, move.Col);
                if (!success)
                {
                    _logger.LogWarning($"Недопустимый ход: строка {move.Row}, столбец {move.Col}");
                    return BadRequest("Недопустимый ход");
                }
                
                return _ticTacToeService.GetGame();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при выполнении хода: строка {move?.Row}, столбец {move?.Col}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("reset")]
        public ActionResult<TicTacToeGame> ResetGame()
        {
            try
            {
                _logger.LogInformation("Сброс игры");
                _ticTacToeService.ResetGame();
                return _ticTacToeService.GetGame();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сбросе игры");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }

    public class MoveRequest
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
} 