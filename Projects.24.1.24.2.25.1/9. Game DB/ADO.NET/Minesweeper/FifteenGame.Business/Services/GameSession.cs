using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services;

public sealed class GameSession
{
    public object SyncRoot { get; } = new object();
    public Guid Id { get; } = Guid.NewGuid();
    public MinesweeperGame Game { get; }
    public string PlayerName { get; }
    public string DifficultyName { get; }
    public int Moves { get; set; }
    public DateTimeOffset? StartedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public bool ResultSaved { get; set; }

    public GameSession(MinesweeperGame game, string playerName, string difficultyName)
    {
        Game = game;
        PlayerName = playerName;
        DifficultyName = difficultyName;
    }

    public int Seconds => StartedAt == null ? 0 :
        (int)Math.Max(0, ((FinishedAt ?? DateTimeOffset.UtcNow) - StartedAt.Value).TotalSeconds);
}
