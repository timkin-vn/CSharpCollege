using Game2048.Common.Definitions;

namespace Game2048.Common.BusinessModels;

/// <summary>
/// Бизнес-модель игровой сессии 2048
/// </summary>
public class GameModel
{
    private readonly int[,] _grid = new int[Constants.GridSize, Constants.GridSize];

    public int Id { get; set; }
    public int UserId { get; set; }
    public int MoveCount { get; set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

    /// <summary>
    /// Доступ к ячейке поля по строке и столбцу
    /// </summary>
    public int this[int row, int col]
    {
        get => _grid[row, col];
        set => _grid[row, col] = value;
    }
}
