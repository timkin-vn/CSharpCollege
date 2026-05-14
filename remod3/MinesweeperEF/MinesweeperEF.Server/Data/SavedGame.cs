namespace MinesweeperEF.Server.Data;

public sealed class SavedGame {
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Name { get; set; } = "Game";
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; } = "InProgress";

    public int Rows { get; set; }
    public int Cols { get; set; }
    public int Mines { get; set; }

    public string StateJson { get; set; } = "{}";
}
