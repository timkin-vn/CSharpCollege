using GameDB.Business.Services;
using GameDB.Common.Constants;
using Xunit;

namespace GameDB.UnitTests;

public class MergeLogicTests
{
    private readonly GameService _service = new();

    [Fact]
    public void MergeLine_ShouldMergeTwoEqualTiles()
    {
        // Arrange
        var line = new[] { 2, 2, 0, 0 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 4, 0, 0, 0 }, result);
        Assert.Equal(4, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldMergeMultipleTiles()
    {
        // Arrange
        var line = new[] { 2, 2, 4, 4 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 4, 8, 0, 0 }, result);
        Assert.Equal(12, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldNotMergeDifferentTiles()
    {
        // Arrange
        var line = new[] { 2, 4, 2, 4 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 2, 4, 2, 4 }, result);
        Assert.Equal(0, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldHandleEmptyLine()
    {
        // Arrange
        var line = new[] { 0, 0, 0, 0 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 0, 0, 0, 0 }, result);
        Assert.Equal(0, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldHandleSingleTile()
    {
        // Arrange
        var line = new[] { 2, 0, 0, 0 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 2, 0, 0, 0 }, result);
        Assert.Equal(0, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldMergeTripleTilesCorrectly()
    {
        // Arrange
        var line = new[] { 2, 2, 2, 0 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 4, 2, 0, 0 }, result);
        Assert.Equal(4, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldMergeFourTilesCorrectly()
    {
        // Arrange
        var line = new[] { 2, 2, 2, 2 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 4, 4, 0, 0 }, result);
        Assert.Equal(8, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldHandleComplexCase()
    {
        // Arrange
        var line = new[] { 2, 2, 4, 8 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 4, 4, 8, 0 }, result);
        Assert.Equal(4, scoreGain);
    }

    [Fact]
    public void MergeLine_ShouldHandleLargeNumbers()
    {
        // Arrange
        var line = new[] { 1024, 1024, 0, 0 };

        // Act
        var result = MergeLineHelper(line, out int scoreGain);

        // Assert
        Assert.Equal(new[] { 2048, 0, 0, 0 }, result);
        Assert.Equal(2048, scoreGain);
    }

    private int[] MergeLineHelper(int[] line, out int scoreGain)
    {
        var tiles = line.Where(v => v != GameConstants.EmptyCellValue).ToList();
        var merged = new int[GameConstants.ColumnCount];
        scoreGain = 0;

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
}