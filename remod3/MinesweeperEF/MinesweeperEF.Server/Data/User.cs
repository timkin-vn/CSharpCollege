namespace MinesweeperEF.Server.Data;

public sealed class User {
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}
