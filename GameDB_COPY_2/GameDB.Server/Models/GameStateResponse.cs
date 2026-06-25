namespace GameDB.Server.Models;

public class GameStateResponse
{
    public int[][] Cells { get; set; } = Array.Empty<int[]>();
    public int Score { get; set; }
    public bool HasWon { get; set; }
}