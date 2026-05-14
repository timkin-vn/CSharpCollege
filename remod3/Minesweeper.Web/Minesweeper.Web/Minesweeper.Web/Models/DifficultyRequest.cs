namespace Minesweeper.Web.Models;

public sealed class DifficultyRequest {
    public string? Preset { get; set; }
    public int? Rows { get; set; }
    public int? Columns { get; set; }
    public int? Mines { get; set; }
}
