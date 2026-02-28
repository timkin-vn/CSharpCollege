using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.Business.Results;

public sealed record GameActionResult(IReadOnlyList<CellUpdate> Updates, bool GameOver, bool HasWon,  bool HitMine);
