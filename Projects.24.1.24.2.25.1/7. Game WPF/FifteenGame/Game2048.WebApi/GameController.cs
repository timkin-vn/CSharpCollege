using Microsoft.AspNetCore.Mvc;
using Game2048.Business.Models;
using Game2048.Business.Services;
using Microsoft.EntityFrameworkCore; // <-- Добавили для работы с методами БД

namespace Game2048.WebApi.Controllers;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;
    private readonly GameDbContext _context;
    private static GameModel _serverGameModel = new GameModel();

    public GameController(GameService gameService, GameDbContext context)
    {
        _gameService = gameService;
        _context = context; 

        if (_serverGameModel.Score == 0 && _serverGameModel.Board[0, 0] == 0)
        {
            _serverGameModel.Reset();
        }
    }

    [HttpPost("restart")]
    public ActionResult<GameModel> Restart()
    {
        _serverGameModel.Reset();
        return Ok(_serverGameModel);
    }

    [HttpPost("move")]
    public ActionResult<GameModel> Move([FromQuery] MoveDirection direction)
    {
        _gameService.MakeMove(_serverGameModel, direction);
        return Ok(_serverGameModel);
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveResult([FromQuery] string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return BadRequest("Имя игрока не может быть пустым.");
        }

        var session = new GameSession
        {
            PlayerName = playerName,
            Score = _serverGameModel.Score, 
            PlayedAt = DateTime.UtcNow
        };

        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Результат игры успешно сохранен!", score = _serverGameModel.Score });
    }

    [HttpGet("leaderboard")]
    public async Task<ActionResult<IEnumerable<GameSession>>> GetLeaderboard()
    {
        var topScores = await _context.GameSessions
            .OrderByDescending(g => g.Score)
            .Take(10)
            .ToListAsync();

        return Ok(topScores);
    }
}
