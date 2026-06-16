using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Server.Data;

public sealed class GameCell {
    public int Id { get; set; }
    public Guid SavedGameId { get; set; }
    public SavedGame SavedGame { get; set; } = null!;
    public int Row { get; set; }
    public int Col { get; set; }
    public bool IsMine { get; set; }
    public int AdjacentMines { get; set; }
    public CellState State { get; set; }
}
