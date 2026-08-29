namespace MinesweeperEF.Contracts.Games;

public sealed record GameActionRequest(GameActionType Type, int Row, int Col, bool DebugMode = false);
