namespace Minesweeper.Web.Models;

public sealed class GameViewModel {
    public int Rows { get; init; }
    public int Columns { get; init; }
    public int Mines { get; init; }
    public int FlagsLeft { get; init; }
    public int Seconds { get; init; }
    public bool GameOver { get; init; }
    public bool HasWon { get; init; }
    public bool HitMine { get; init; }
    public bool HasStarted { get; init; }
    public string Difficulty { get; init; } = string.Empty;
    public string DifficultyKey { get; init; } = string.Empty;
    public string StatusText { get; init; } = string.Empty;
    public IReadOnlyList<IReadOnlyList<CellViewModel>> Cells { get; init; } = Array.Empty<IReadOnlyList<CellViewModel>>();
}
