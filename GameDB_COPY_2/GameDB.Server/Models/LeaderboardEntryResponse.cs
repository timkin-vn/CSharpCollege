namespace GameDB.Server.Models;

public class LeaderboardEntryResponse
{
    public string Username { get; set; } = string.Empty;
    public int HighScore { get; set; }
    public int MaxTile { get; set; }
    public string AchievedAt { get; set; } = string.Empty;
}