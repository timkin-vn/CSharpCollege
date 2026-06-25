using GameDB.Business.Models;

namespace GameDB.Business.Services;

public class GameService
{
    private readonly Random _random = new();

    public void Initialize(GameModel model)
    {
        model.Score = 0;
        model.HasWon = false;

        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                model[row, column] = GameModel.EmptyCellValue;
            }
        }

        AddRandomTile(model);
        AddRandomTile(model);
    }

    public bool MakeMove(GameModel model, MoveDirection direction)
    {
        var before = Clone(model);
        int scoreGain = 0;

        switch (direction)
        {
            case MoveDirection.Left:
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    var line = GetRow(model, row);
                    var merged = MergeLine(line, out int gain);
                    scoreGain += gain;
                    SetRow(model, row, merged);
                }
                break;

            case MoveDirection.Right:
                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    var line = GetRow(model, row).Reverse().ToArray();
                    var merged = MergeLine(line, out int gain);
                    scoreGain += gain;
                    SetRow(model, row, merged.Reverse().ToArray());
                }
                break;

            case MoveDirection.Up:
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    var line = GetColumn(model, column);
                    var merged = MergeLine(line, out int gain);
                    scoreGain += gain;
                    SetColumn(model, column, merged);
                }
                break;

            case MoveDirection.Down:
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    var line = GetColumn(model, column).Reverse().ToArray();
                    var merged = MergeLine(line, out int gain);
                    scoreGain += gain;
                    SetColumn(model, column, merged.Reverse().ToArray());
                }
                break;

            default:
                return false;
        }

        if (AreEqual(before, model))
        {
            return false;
        }

        model.Score += scoreGain;
        AddRandomTile(model);
        UpdateWinState(model);
        return true;
    }

    public void ContinueAfterWin(GameModel model)
    {
        model.HasWon = false;
    }

    public bool IsGameOver(GameModel model)
    {
        return !CanMove(model);
    }

    private static int[] MergeLine(int[] line, out int scoreGain)
    {
        scoreGain = 0;
        var tiles = line.Where(value => value != GameModel.EmptyCellValue).ToList();
        var merged = new int[GameModel.ColumnCount];

        int index = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (i + 1 < tiles.Count && tiles[i] == tiles[i + 1])
            {
                int newValue = tiles[i] * 2;
                merged[index++] = newValue;
                scoreGain += newValue;
                i++;
            }
            else
            {
                merged[index++] = tiles[i];
            }
        }

        return merged;
    }

    private void AddRandomTile(GameModel model)
    {
        var emptyCells = new List<(int row, int column)>();
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                if (model[row, column] == GameModel.EmptyCellValue)
                {
                    emptyCells.Add((row, column));
                }
            }
        }

        if (emptyCells.Count == 0)
        {
            return;
        }

        var cell = emptyCells[_random.Next(emptyCells.Count)];
        model[cell.row, cell.column] = _random.Next(10) == 0 ? 4 : 2;
    }

    private static void UpdateWinState(GameModel model)
    {
        if (model.HasWon)
        {
            return;
        }

        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                if (model[row, column] >= GameModel.WinTileValue)
                {
                    model.HasWon = true;
                    return;
                }
            }
        }
    }

    private static bool CanMove(GameModel model)
    {
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                int value = model[row, column];
                if (value == GameModel.EmptyCellValue)
                {
                    return true;
                }

                if (column + 1 < GameModel.ColumnCount && model[row, column + 1] == value)
                {
                    return true;
                }

                if (row + 1 < GameModel.RowCount && model[row + 1, column] == value)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static int[] GetRow(GameModel model, int row)
    {
        var line = new int[GameModel.ColumnCount];
        for (int column = 0; column < GameModel.ColumnCount; column++)
        {
            line[column] = model[row, column];
        }

        return line;
    }

    private static void SetRow(GameModel model, int row, int[] values)
    {
        for (int column = 0; column < GameModel.ColumnCount; column++)
        {
            model[row, column] = values[column];
        }
    }

    private static int[] GetColumn(GameModel model, int column)
    {
        var line = new int[GameModel.RowCount];
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            line[row] = model[row, column];
        }

        return line;
    }

    private static void SetColumn(GameModel model, int column, int[] values)
    {
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            model[row, column] = values[row];
        }
    }

    private static int[,] Clone(GameModel model)
    {
        var copy = new int[GameModel.RowCount, GameModel.ColumnCount];
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                copy[row, column] = model[row, column];
            }
        }

        return copy;
    }

    private static bool AreEqual(int[,] before, GameModel after)
    {
        for (int row = 0; row < GameModel.RowCount; row++)
        {
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                if (before[row, column] != after[row, column])
                {
                    return false;
                }
            }
        }

        return true;
    }
}