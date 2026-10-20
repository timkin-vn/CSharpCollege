using System.Collections.Concurrent;
using FifteenGame.Business.Models;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;

namespace FifteenGame.Business.Services;

public sealed class MinesweeperDbService
{
    private readonly ConcurrentDictionary<Guid, GameSession> _games = new();
    private readonly IGameResultRepository _repository;
    private readonly MinesweeperService _gameService = new();

    public MinesweeperDbService(IGameResultRepository repository)
    {
        _repository = repository;
    }

    public GameState CreateGame(string? playerName, string? difficulty, bool safeFirstMove)
    {
        int rows = 9, columns = 9, mines = 10;
        string name = "Лёгкий";
        if (difficulty == "medium")
        {
            rows = 12; columns = 12; mines = 25; name = "Средний";
        }
        else if (difficulty == "hard")
        {
            rows = 16; columns = 16; mines = 50; name = "Тяжёлый";
        }

        string player = string.IsNullOrWhiteSpace(playerName) ? "Игрок" : playerName.Trim();
        if (player.Length > 40) player = player.Substring(0, 40);

        var session = new GameSession(_gameService.CreateGame(rows, columns, mines, safeFirstMove), player, name);
        _games[session.Id] = session;
        return ToState(session);
    }

    public GameState? GetGame(Guid id)
    {
        if (!_games.TryGetValue(id, out var session)) return null;
        lock (session.SyncRoot)
        {
            return ToState(session);
        }
    }

    public GameState? Reveal(Guid id, int row, int column)
    {
        if (!_games.TryGetValue(id, out var session)) return null;
        lock (session.SyncRoot)
        {
            if (_gameService.OpenCell(session.Game, row, column))
            {
                if (session.StartedAt == null) session.StartedAt = DateTimeOffset.UtcNow;
                session.Moves++;
                if (session.Game.IsFinished && session.FinishedAt == null)
                    session.FinishedAt = DateTimeOffset.UtcNow;
            }
            SaveFinishedGame(session);
            return ToState(session);
        }
    }

    public GameState? ToggleFlag(Guid id, int row, int column)
    {
        if (!_games.TryGetValue(id, out var session)) return null;
        lock (session.SyncRoot)
        {
            _gameService.ToggleFlag(session.Game, row, column);
            return ToState(session);
        }
    }

    private void SaveFinishedGame(GameSession session)
    {
        if (!session.Game.IsFinished || session.ResultSaved) return;
        _repository.Save(new GameResultDto
        {
            PlayerName = session.PlayerName,
            Difficulty = session.DifficultyName,
            FirstMoveMode = session.Game.SafeFirstMove ? "Безопасный" : "Опасный",
            IsWin = session.Game.IsWon,
            Seconds = session.Seconds,
            Moves = session.Moves,
            Rows = session.Game.Rows,
            Columns = session.Game.Columns,
            Mines = session.Game.MinesCount,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });
        session.ResultSaved = true;
    }

    private static GameState ToState(GameSession session)
    {
        var game = session.Game;
        var cells = new List<CellState>(game.Rows * game.Columns);
        for (int row = 0; row < game.Rows; row++)
        {
            for (int column = 0; column < game.Columns; column++)
            {
                MinesweeperCell cell = game[row, column];
                cells.Add(new CellState(
                    row, column, cell.IsOpened, cell.IsFlagged,
                    game.IsFinished && cell.IsMine,
                    cell.IsOpened ? cell.MinesAround : 0));
            }
        }
        return new GameState(session.Id, game.Rows, game.Columns, game.MinesCount,
            game.FlagsCount, session.Moves, session.Seconds, game.SafeFirstMove,
            game.IsFinished, game.IsWon, session.ResultSaved, session.DifficultyName, cells);
    }
}

public sealed record CellState(int Row, int Column, bool IsOpened, bool IsFlagged, bool IsMine, int MinesAround);
public sealed record GameState(Guid Id, int Rows, int Columns, int Mines, int Flags, int Moves,
    int Seconds, bool SafeFirstMove, bool IsFinished, bool IsWon, bool ResultSaved,
    string DifficultyName, IReadOnlyList<CellState> Cells);
