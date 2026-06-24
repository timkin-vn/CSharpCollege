namespace GameDB.Common.DTOs;

public class SaveGameRequest
{
    public int PlayerId { get; set; }
    public int Score { get; set; }
    public int[][] GameField { get; set; } = Array.Empty<int[]>();
}