using Microsoft.AspNetCore.Mvc;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService = new();
    private static GameModel? _game;

    [HttpPost("start")]
    public IActionResult Start()
    {
        _game = new GameModel();
        _gameService.Initialize(_game);
        return Ok(ToDto(_game));
    }

    [HttpPost("move")]
    public IActionResult Move([FromBody] MoveRequest request)
    {
        if (_game == null) return BadRequest("Start game first");

        MoveDirection direction;
        switch (request.Direction?.ToLower())
        {
            case "left":
                direction = MoveDirection.Left;
                break;
            case "right":
                direction = MoveDirection.Right;
                break;
            case "up":
                direction = MoveDirection.Up;
                break;
            case "down":
                direction = MoveDirection.Down;
                break;
            default:
                return BadRequest("Invalid direction");
        }

        _gameService.MakeMove(_game, direction);

        return Ok(new
        {
            GameState = ToDto(_game),
            IsWin = _game.HasWon,
            IsGameOver = _gameService.IsGameOver(_game)
        });
    }

    [HttpPost("continue")]
    public IActionResult Continue()
    {
        if (_game == null) return BadRequest("Start game first");
        _gameService.ContinueAfterWin(_game);
        return Ok(ToDto(_game));
    }

    private object ToDto(GameModel model)
    {
        var cells = new int[4][];
        for (int i = 0; i < 4; i++)
        {
            cells[i] = new int[4];
            for (int j = 0; j < 4; j++)
                cells[i][j] = model[i, j];
        }

        return new { Cells = cells, Score = model.Score, HasWon = model.HasWon };
    }
}

public class MoveRequest
{
    public string? Direction { get; set; }
}