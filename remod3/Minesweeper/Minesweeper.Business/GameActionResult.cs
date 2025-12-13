namespace Minesweeper.Business;

public sealed record GameActionResult(IReadOnlyList<CellUpdate> Updates, bool GameOver, bool HasWon,  bool HitMine);
