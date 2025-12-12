using Minesweeper.Business;

namespace Minesweeper.Web.Business;

public sealed record GameActionResult(IReadOnlyList<CellUpdate> Updates, bool GameOver, bool HasWon,  bool HitMine);
