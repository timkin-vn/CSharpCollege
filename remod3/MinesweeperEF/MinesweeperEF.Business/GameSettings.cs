namespace MinesweeperEF.Business;

public sealed record GameSettings(int Rows, int Columns, int Mines) {
    public static GameSettings Beginner() => new(9, 9, 10);
    public static GameSettings Intermediate() => new(16, 16, 40);
    public static GameSettings Expert() => new(16, 30, 99);
}
