namespace FifteenGame.Common.Dtos;

public sealed class GameResultDto
{
    public long Id { get; set; }
    public string PlayerName { get; set; } = "";
    public string Difficulty { get; set; } = "";
    public string FirstMoveMode { get; set; } = "";
    public bool IsWin { get; set; }
    public int Seconds { get; set; }
    public int Moves { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int Mines { get; set; }
    public string CreatedAt { get; set; } = "";
}
