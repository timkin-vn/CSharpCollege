using Minesweeper.Web.Business.Cells;

namespace Minesweeper.Web.Business.Results;

public sealed record GameActionResult(IReadOnlyList<CellUpdate> Updates, bool GameOver, bool HasWon, bool HitMine);
