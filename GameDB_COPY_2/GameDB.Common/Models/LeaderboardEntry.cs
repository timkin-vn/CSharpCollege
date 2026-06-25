namespace GameDB.Common.Models;

public class LeaderboardEntry
{
    public int ScoreId { get; set; }
    public int PlayerId { get; set; }
    public string? Username { get; set; }
    public int HighScore { get; set; }
    public int MaxTile { get; set; }
    public DateTime AchievedAt { get; set; }
}