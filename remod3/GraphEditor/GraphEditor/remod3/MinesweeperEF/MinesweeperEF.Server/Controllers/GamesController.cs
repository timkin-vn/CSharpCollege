using System.Text.Json;
using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Core;
using MinesweeperEF.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinesweeperEF.Contracts.Games;
using MinesweeperEF.Server.Data;

namespace MinesweeperEF.Server.Controllers;

[ApiController]
[Route("api/games")]
[Authorize]
public sealed class GamesController(AppDbContext db) : ControllerBase {
    private async Task<User> GetCurrentUser() {
        var userName = User.Identity?.Name;
        if (string.IsNullOrWhiteSpace(userName))
            throw new UnauthorizedAccessException();

        var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user is null)
            throw new UnauthorizedAccessException();

        return user;
    }

    [HttpGet]
    public async Task<List<SavedGameInfoDto>> List() {
        var user = await GetCurrentUser();

        return await db.SavedGames
            .Where(g => g.UserId == user.Id)
            .OrderByDescending(g => g.UpdatedAt)
            .Select(g => new SavedGameInfoDto(g.Id, g.Name))
            .ToListAsync();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameSnapshotDto>> Get(Guid id) {
        var user = await GetCurrentUser();
        var game = await db.SavedGames.FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);
        if (game is null) return NotFound();

        var dto = JsonSerializer.Deserialize<GameStateDto>(game.StateJson);
        if (dto is null) return Problem("Invalid game state");

        return new GameSnapshotDto(
            game.Id,
            dto.Settings.Rows,
            dto.Settings.Columns,
            dto.FlagsLeft,
            dto.GameOver,
            dto.HasWon,
            game.StateJson
        );
    }

    [HttpPost]
    public async Task<ActionResult<GameSnapshotDto>> NewGame(NewGameRequest req) {
        var user = await GetCurrentUser();
        var settings = new GameSettings(req.Rows, req.Cols, req.Mines);
        var board = new GameBoard();
        board.ApplySettings(settings);
        board.NewGame();
        
        var dto = board.ExportState();
        var json = JsonSerializer.Serialize(dto);

        var game = new SavedGame {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Name = string.IsNullOrWhiteSpace(req.Name) ? "Game" : req.Name!,
            UpdatedAt = DateTime.UtcNow,
            Rows = req.Rows,
            Cols = req.Cols,
            Mines = req.Mines,
            Status = "InProgress",
            StateJson = json
        };

        db.SavedGames.Add(game);
        await db.SaveChangesAsync();

        return new GameSnapshotDto(
            game.Id,
            game.Rows,
            game.Cols,
            board.FlagsLeft,
            board.GameOver,
            board.HasWon,
            game.StateJson
        );
    }
    
    [HttpPost("{id:guid}/action")]
    public async Task<ActionResult<GameSnapshotDto>> Action(Guid id, GameActionRequest req) {
        var user = await GetCurrentUser();

        var game = await db.SavedGames.FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);
        if (game is null) return NotFound();

        var dto = JsonSerializer.Deserialize<GameStateDto>(game.StateJson);
        if (dto is null) return Problem("Invalid game state");

        var board = GameBoard.ImportState(dto);
        
        var allowAfterLoss = req.DebugMode && !board.HasWon;

        if (req.Type == GameActionType.RevealMines && (!board.GameOver || board.HasWon))
            return BadRequest("Показ мин доступен только после поражения");

        switch (req.Type) {
            case GameActionType.Reveal:
                board.Reveal(req.Row, req.Col, allowAfterLoss);
                break;
            
            case GameActionType.ToggleFlag:
                board.ToggleFlag(req.Row, req.Col, allowAfterLoss);
                break;
            
            case GameActionType.Chord:
                board.Chord(req.Row, req.Col, allowAfterLoss);
                break;
            
            case GameActionType.RevealMines:
                board.RevealAllMines();
                break;
            
            default:
                return BadRequest($"Unknown action type: {req.Type}");
        }

        game.UpdatedAt = DateTime.UtcNow;

        game.Status = board.GameOver ? board.HasWon ? "Won" : "Lost" : "InProgress";

        game.StateJson = JsonSerializer.Serialize(board.ExportState());
        await db.SaveChangesAsync();

        return new GameSnapshotDto(
            game.Id,
            board.Settings.Rows,
            board.Settings.Columns,
            board.FlagsLeft,
            board.GameOver,
            board.HasWon,
            game.StateJson
        );
    }
}
