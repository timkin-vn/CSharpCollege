namespace MinesweeperEF.Contracts.Games;

public sealed record NewGameRequest(int Rows, int Cols, int Mines, string? Name);
