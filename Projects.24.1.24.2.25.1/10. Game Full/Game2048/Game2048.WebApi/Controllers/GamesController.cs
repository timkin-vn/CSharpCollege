using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace Game2048.WebApi.Controllers;

[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("api/games/get-by-game-id/{gameId}")]
    public ActionResult<GameReply> GetByGameId(int gameId)
    {
        var model = _gameService.GetByGameId(gameId);
        return model == null ? NotFound() : ToDto(model);
    }

    [HttpGet("api/games/get-by-user-id/{userId}")]
    public GameReply GetByUserId(int userId)
        => ToDto(_gameService.GetByUserId(userId));

    [HttpGet("api/games/is-over/{gameId}")]
    public BooleanReply IsGameOver(int gameId)
        => new() { IsTrue = _gameService.IsGameOver(gameId) ?? false };

    [HttpGet("api/games/is-won/{gameId}")]
    public BooleanReply IsGameWon(int gameId)
        => new() { IsTrue = _gameService.IsGameWon(gameId) ?? false };

    [HttpPost("api/games/make-move")]
    public ActionResult<GameReply> MakeMove([FromBody] MakeMoveRequest request)
    {
        if (!Enum.TryParse<MoveDirection>(request.Direction, out var direction))
            return BadRequest($"Unknown direction: {request.Direction}");

        return ToDto(_gameService.MakeMove(request.GameId, direction));
    }

    [HttpDelete("api/games/remove/{gameId}")]
    public IActionResult RemoveGame(int gameId)
    {
        _gameService.RemoveGame(gameId);
        return NoContent();
    }

    // ── helpers ───────────────────────────────────────────────────────────

    private static GameReply ToDto(GameModel model)
    {
        var dto = new GameReply
        {
            Id = model.Id,
            UserId = model.UserId,
            MoveCount = model.MoveCount,
            Score = model.Score,
            IsWon = model.IsWon,
            Cells = new int[Constants.GridSize * Constants.GridSize],
        };

        int idx = 0;
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                dto.Cells[idx++] = model[r, c];

        return dto;
    }
}
