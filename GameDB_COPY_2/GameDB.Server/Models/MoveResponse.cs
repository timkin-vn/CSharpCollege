namespace GameDB.Server.Models;

public class MoveResponse
{
    public int[][] Cells { get; set; } = Array.Empty<int[]>();
    public int Score { get; set; }
    public int ScoreGain { get; set; }
    public bool IsGameOver { get; set; }
    public bool HasWon { get; set; }
}