using MinesweeperEF.Business.Cells;

namespace MinesweeperEF.Business.Models;

public sealed record GameStateDto(
    GameSettings Settings,
    int FlagsLeft,
    bool GameOver,
    bool HasWon,
    bool HasStarted,
    CellDto[] Cells
);

public sealed record CellDto(
    bool IsMine,
    int AdjacentMines,
    CellState State
);
