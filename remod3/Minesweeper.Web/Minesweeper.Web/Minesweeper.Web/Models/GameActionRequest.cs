namespace Minesweeper.Web.Models;

public sealed class GameActionRequest {
    public string Action { get; set; } = "reveal";
    public int Row { get; set; }
    public int Column { get; set; }
}
