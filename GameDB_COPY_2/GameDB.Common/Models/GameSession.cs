namespace GameDB.Common.Models;

public class GameSession
{
    public int SessionId { get; set; }
    public int PlayerId { get; set; }
    public int CurrentScore { get; set; }
    public int[][] GameField { get; set; } = Array.Empty<int[]>();
    public DateTime UpdatedAt { get; set; }
}