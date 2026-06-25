using GameDB.Business.Models;
using GameDB.Business.Services;
using GameDB.Common.Constants;
using GameDB.Common.Enums;
using Xunit;
using MoveDirection = GameDB.Business.Models.MoveDirection;

namespace GameDB.UnitTests;

public class GameServiceTests
{
    private readonly GameService _service = new();

    [Fact]
    public void Initialize_ShouldSetScoreToZero()
    {
        // Arrange
        var model = new GameModel();

        // Act
        _service.Initialize(model);

        // Assert
        Assert.Equal(0, model.Score);
        Assert.False(model.HasWon);
    }

    [Fact]
    public void Initialize_ShouldAddTwoTiles()
    {
        // Arrange
        var model = new GameModel();

        // Act
        _service.Initialize(model);

        // Assert
        int tileCount = 0;
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                if (model[row, col] != GameConstants.EmptyCellValue)
                    tileCount++;
            }
        }
        Assert.Equal(2, tileCount);
    }

    [Fact]
    public void MakeMove_Left_ShouldMergeTiles()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        model[0, 0] = 2;
        model[0, 1] = 2;

        // Act
        var result = _service.MakeMove(model, MoveDirection.Left);

        // Assert
        Assert.True(result);
        Assert.Equal(4, model[0, 0]);
        Assert.Equal(0, model[0, 1]);
        Assert.Equal(4, model.Score);
    }

    [Fact]
    public void MakeMove_Right_ShouldMergeTiles()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        model[0, 2] = 2;
        model[0, 3] = 2;

        // Act
        var result = _service.MakeMove(model, MoveDirection.Right);

        // Assert
        Assert.True(result);
        Assert.Equal(0, model[0, 2]);
        Assert.Equal(4, model[0, 3]);
        Assert.Equal(4, model.Score);
    }

    [Fact]
    public void MakeMove_Up_ShouldMergeTiles()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        model[0, 0] = 2;
        model[1, 0] = 2;

        // Act
        var result = _service.MakeMove(model, MoveDirection.Up);

        // Assert
        Assert.True(result);
        Assert.Equal(4, model[0, 0]);
        Assert.Equal(0, model[1, 0]);
        Assert.Equal(4, model.Score);
    }

    [Fact]
    public void MakeMove_Down_ShouldMergeTiles()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        model[2, 0] = 2;
        model[3, 0] = 2;

        // Act
        var result = _service.MakeMove(model, MoveDirection.Down);

        // Assert
        Assert.True(result);
        Assert.Equal(0, model[2, 0]);
        Assert.Equal(4, model[3, 0]);
        Assert.Equal(4, model.Score);
    }

    [Fact]
    public void MakeMove_ShouldNotChangeBoard_WhenNoValidMoves()
    {
        // Arrange
        var model = new GameModel();
        // Заполняем поле значениями, где нет ходов
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                model[row, col] = (row + col) % 2 == 0 ? 2 : 4;
            }
        }
        var copy = CloneModel(model);

        // Act
        var result = _service.MakeMove(model, MoveDirection.Left);

        // Assert
        Assert.False(result);
        Assert.Equal(copy, model);
    }

    [Fact]
    public void IsGameOver_ShouldReturnTrue_WhenNoMovesAvailable()
    {
        // Arrange
        var model = new GameModel();
        int value = 2;
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                model[row, col] = value;
                value = value == 2 ? 4 : 2;
            }
        }

        // Act
        var result = _service.IsGameOver(model);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsGameOver_ShouldReturnFalse_WhenMovesAvailable()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);

        // Act
        var result = _service.IsGameOver(model);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ContinueAfterWin_ShouldResetHasWon()
    {
        // Arrange
        var model = new GameModel();
        model.HasWon = true;

        // Act
        _service.ContinueAfterWin(model);

        // Assert
        Assert.False(model.HasWon);
    }

    [Fact]
    public void MakeMove_ShouldUpdateScore()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        model[0, 0] = 2;
        model[0, 1] = 2;

        // Act
        _service.MakeMove(model, MoveDirection.Left);

        // Assert
        Assert.Equal(4, model.Score);
    }

    [Fact]
    public void MakeMove_ShouldAddNewTile()
    {
        // Arrange
        var model = new GameModel();
        _service.Initialize(model);
        
        // Убираем все плитки кроме одной
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                model[row, col] = 0;
            }
        }
        model[0, 0] = 2;
        model[0, 1] = 2;

        // Act
        _service.MakeMove(model, MoveDirection.Left);

        // Assert - должна появиться новая плитка
        int tileCount = 0;
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                if (model[row, col] != 0) tileCount++;
            }
        }
        Assert.Equal(2, tileCount); // 4 + новая плитка
    }

    private GameModel CloneModel(GameModel source)
    {
        var clone = new GameModel();
        for (int row = 0; row < GameConstants.RowCount; row++)
        {
            for (int col = 0; col < GameConstants.ColumnCount; col++)
            {
                clone[row, col] = source[row, col];
            }
        }
        return clone;
    }
}