namespace Game2048.DataAccess.Models;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<GameEntity> Games { get; set; } = [];
}

public class GameEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public int MoveCount { get; set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

    public string GridData { get; set; } = string.Empty;
}
