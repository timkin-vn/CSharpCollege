namespace MinesweeperEF.Contracts.Games;

public sealed record GameSnapshotDto(
    Guid GameId,
    int Rows,
    int Cols,
    int FlagsLeft,
    bool GameOver,
    bool HasWon,
    CellSnapshotDto[] Cells
);
