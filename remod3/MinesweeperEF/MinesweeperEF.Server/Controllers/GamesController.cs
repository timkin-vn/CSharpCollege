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
        var game = await db.SavedGames
            .Include(g => g.Cells)
            .FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);
        if (game is null) return NotFound();

        var cells = game.Cells
            .OrderBy(c => c.Row).ThenBy(c => c.Col)
            .Select(c => new CellSnapshotDto(c.IsMine, c.AdjacentMines, c.State))
            .ToArray();

        return new GameSnapshotDto(game.Id, game.Rows, game.Cols, game.FlagsLeft, game.GameOver, game.HasWon, cells);
    }

    [HttpPost]
    public async Task<ActionResult<GameSnapshotDto>> NewGame(NewGameRequest req) {
        var user = await GetCurrentUser();
        var settings = new GameSettings(req.Rows, req.Cols, req.Mines);
        var board = new GameBoard();
        board.ApplySettings(settings);
        board.NewGame();

        var state = board.ExportState();

        var game = new SavedGame {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Name = string.IsNullOrWhiteSpace(req.Name) ? "Game" : req.Name!,
            UpdatedAt = DateTime.UtcNow,
            Rows = req.Rows,
            Cols = req.Cols,
            Mines = req.Mines,
            Status = "InProgress",
            FlagsLeft = state.FlagsLeft,
            HasStarted = state.HasStarted,
            GameOver = state.GameOver,
            HasWon = state.HasWon
        };

        for (var i = 0; i < state.Cells.Length; i++) {
            var d = state.Cells[i];
            game.Cells.Add(new GameCell {
                SavedGameId = game.Id,
                Row = i / req.Cols,
                Col = i % req.Cols,
                IsMine = d.IsMine,
                AdjacentMines = d.AdjacentMines,
                State = d.State
            });
        }

        db.SavedGames.Add(game);
        await db.SaveChangesAsync();

        var snapshotCells = state.Cells
            .Select(c => new CellSnapshotDto(c.IsMine, c.AdjacentMines, c.State))
            .ToArray();

        return new GameSnapshotDto(game.Id, game.Rows, game.Cols, board.FlagsLeft, board.GameOver, board.HasWon, snapshotCells);
    }

    [HttpPost("{id:guid}/action")]
    public async Task<ActionResult<GameSnapshotDto>> Action(Guid id, GameActionRequest req) {
        var user = await GetCurrentUser();

        var game = await db.SavedGames
            .Include(g => g.Cells)
            .FirstOrDefaultAsync(g => g.Id == id && g.UserId == user.Id);
        if (game is null) return NotFound();

        var orderedCells = game.Cells.OrderBy(c => c.Row).ThenBy(c => c.Col).ToList();
        var cellDtos = orderedCells
            .Select(c => new CellDto(c.IsMine, c.AdjacentMines, c.State))
            .ToArray();

        var stateDto = new GameStateDto(
            new GameSettings(game.Rows, game.Cols, game.Mines),
            game.FlagsLeft, game.GameOver, game.HasWon, game.HasStarted,
            cellDtos
        );
        var board = GameBoard.ImportState(stateDto);

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

        var newState = board.ExportState();

        game.FlagsLeft = newState.FlagsLeft;
        game.HasStarted = newState.HasStarted;
        game.GameOver = newState.GameOver;
        game.HasWon = newState.HasWon;
        game.UpdatedAt = DateTime.UtcNow;
        game.Status = board.GameOver ? board.HasWon ? "Won" : "Lost" : "InProgress";

        for (var i = 0; i < newState.Cells.Length; i++) {
            orderedCells[i].IsMine = newState.Cells[i].IsMine;
            orderedCells[i].AdjacentMines = newState.Cells[i].AdjacentMines;
            orderedCells[i].State = newState.Cells[i].State;
        }

        await db.SaveChangesAsync();

        var snapshotCells = newState.Cells
            .Select(c => new CellSnapshotDto(c.IsMine, c.AdjacentMines, c.State))
            .ToArray();

        return new GameSnapshotDto(game.Id, board.Settings.Rows, board.Settings.Columns, board.FlagsLeft, board.GameOver, board.HasWon, snapshotCells);
    }
}
