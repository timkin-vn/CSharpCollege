namespace MinesweeperEF.Contracts.Games;

public sealed record CellSnapshotDto(bool IsMine, int AdjacentMines, CellState State);
