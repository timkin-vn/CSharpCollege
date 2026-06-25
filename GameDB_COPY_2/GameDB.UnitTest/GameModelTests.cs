using GameDB.Business.Models;
using GameDB.Common.Constants;
using Xunit;

namespace GameDB.UnitTests;

public class GameModelTests
{
    [Fact]
    public void GameModel_ShouldInitializeWithEmptyBoard()
    {
        // Arrange & Act
        var model = new GameModel();

        // Assert
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                Assert.Equal(GameConstants.EmptyCellValue, model[row, col]);
            }
        }
    }

    [Fact]
    public void GameModel_ShouldSetAndGetValues()
    {
        // Arrange
        var model = new GameModel();

        // Act
        model[0, 0] = 2;
        model[1, 1] = 4;
        model[2, 2] = 8;
        model[3, 3] = 16;

        // Assert
        Assert.Equal(2, model[0, 0]);
        Assert.Equal(4, model[1, 1]);
        Assert.Equal(8, model[2, 2]);
        Assert.Equal(16, model[3, 3]);
    }

    [Fact]
    public void GameModel_ShouldUpdateScore()
    {
        // Arrange
        var model = new GameModel();

        // Act
        model.Score = 100;

        // Assert
        Assert.Equal(100, model.Score);
    }

    [Fact]
    public void GameModel_ShouldUpdateHasWon()
    {
        // Arrange
        var model = new GameModel();

        // Act
        model.HasWon = true;

        // Assert
        Assert.True(model.HasWon);
    }

    [Fact]
    public void GameModel_ShouldReturnEmptyCellValue()
    {
        // Arrange
        var model = new GameModel();

        // Assert
        Assert.Equal(0, model[0, 0]);
        Assert.Equal(0, model[1, 1]);
        Assert.Equal(0, model[2, 2]);
        Assert.Equal(0, model[3, 3]);
    }
}