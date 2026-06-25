namespace GameDB.Common.DTOs;

public class LeaderboardResponse
{
    public int Rank { get; set; }
    public string Username { get; set; } = string.Empty;
    public int HighScore { get; set; }
    public int MaxTile { get; set; }
    public DateTime AchievedAt { get; set; }
}