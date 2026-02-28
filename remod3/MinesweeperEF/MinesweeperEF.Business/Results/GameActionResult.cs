using MinesweeperEF.Business.Cells;

namespace MinesweeperEF.Business.Results;

public sealed record GameActionResult(IReadOnlyList<CellUpdate> Updates, bool GameOver, bool HasWon, bool HitMine);
