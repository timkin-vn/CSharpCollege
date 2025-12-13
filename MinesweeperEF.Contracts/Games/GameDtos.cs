namespace MinesweeperEF.Contracts.Games;

public enum GameActionType { Reveal, ToggleFlag, Chord, RevealMines }

public sealed record NewGameRequest(int Rows, int Cols, int Mines, string? Name);

public sealed record GameActionRequest(GameActionType Type, int Row, int Col, bool DebugMode = false);

public sealed record SavedGameInfoDto(Guid GameId, string Name, DateTime UpdatedAt, string Status);

public sealed record GameSnapshotDto (
    Guid GameId,
    int Rows,
    int Cols,
    int FlagsLeft,
    bool GameOver,
    bool HasWon,
    string StateJson
);
