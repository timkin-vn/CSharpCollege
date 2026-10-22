using Game2048.Business.Services;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Definitions;
using Moq;

namespace Game2048.UnitTests.ServiceTests;

[TestClass]
public class GameServiceTests
{
    private readonly Mock<IGameRepository> _repoMock = new();
    private readonly GameService _service;

    public GameServiceTests()
    {
        _service = new GameService(_repoMock.Object);
    }

    // ── MergeLine ──────────────────────────────────────────────────────────

    [TestMethod]
    public void MergeLine_EmptyLine_ReturnsZeros()
    {
        var result = GameService.MergeLine(new[] { 0, 0, 0, 0 }, out int score, out bool moved);
        CollectionAssert.AreEqual(new[] { 0, 0, 0, 0 }, result);
        Assert.AreEqual(0, score);
        Assert.IsFalse(moved);
    }

    [TestMethod]
    public void MergeLine_TwoEqualTiles_MergesCorrectly()
    {
        var result = GameService.MergeLine(new[] { 2, 2, 0, 0 }, out int score, out bool moved);
        CollectionAssert.AreEqual(new[] { 4, 0, 0, 0 }, result);
        Assert.AreEqual(4, score);
        Assert.IsTrue(moved);
    }

    [TestMethod]
    public void MergeLine_FourEqualTiles_MergesIntoPairs()
    {
        var result = GameService.MergeLine(new[] { 4, 4, 4, 4 }, out int score, out bool moved);
        CollectionAssert.AreEqual(new[] { 8, 8, 0, 0 }, result);
        Assert.AreEqual(16, score);
        Assert.IsTrue(moved);
    }

    [TestMethod]
    public void MergeLine_TilesWithGap_CompressesFirst()
    {
        var result = GameService.MergeLine(new[] { 2, 0, 2, 0 }, out int score, out bool moved);
        CollectionAssert.AreEqual(new[] { 4, 0, 0, 0 }, result);
        Assert.AreEqual(4, score);
        Assert.IsTrue(moved);
    }

    [TestMethod]
    public void MergeLine_NoMerge_NoDuplicatesMerge()
    {
        var result = GameService.MergeLine(new[] { 2, 4, 8, 16 }, out int score, out bool moved);
        CollectionAssert.AreEqual(new[] { 2, 4, 8, 16 }, result);
        Assert.AreEqual(0, score);
        Assert.IsFalse(moved);
    }

    [TestMethod]
    public void MergeLine_PartialFill_ShiftsLeft()
    {
        var result = GameService.MergeLine(new[] { 0, 0, 0, 4 }, out _, out bool moved);
        CollectionAssert.AreEqual(new[] { 4, 0, 0, 0 }, result);
        Assert.IsTrue(moved);
    }

    // ── MakeMove ───────────────────────────────────────────────────────────

    [TestMethod]
    public void MakeMove_Left_MergesRowsLeft()
    {
        var game = BuildGame(new[,]
        {
            { 2, 2, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        });

        _repoMock.Setup(r => r.GetByGameId(1)).Returns(game);
        _repoMock.Setup(r => r.Update(It.IsAny<GameModel>())).Returns<GameModel>(g => g);

        var result = _service.MakeMove(1, MoveDirection.Left);

        Assert.AreEqual(4, result[0, 0]);
        Assert.AreEqual(0, result[0, 1]);
    }

    [TestMethod]
    public void MakeMove_Right_MergesRowsRight()
    {
        var game = BuildGame(new[,]
        {
            { 0, 0, 2, 2 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        });

        _repoMock.Setup(r => r.GetByGameId(1)).Returns(game);
        _repoMock.Setup(r => r.Update(It.IsAny<GameModel>())).Returns<GameModel>(g => g);

        var result = _service.MakeMove(1, MoveDirection.Right);

        Assert.AreEqual(4, result[0, 3]);
        Assert.AreEqual(0, result[0, 2]);
    }

    [TestMethod]
    public void MakeMove_IncrementsMoveCount()
    {
        var game = BuildGame(new[,]
        {
            { 2, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        });

        _repoMock.Setup(r => r.GetByGameId(1)).Returns(game);
        _repoMock.Setup(r => r.Update(It.IsAny<GameModel>())).Returns<GameModel>(g => g);

        // Первый ход (Left) не должен двигать 2 влево, т.к. она уже слева — ход не засчитается
        // Но Down сдвинет её вниз — ход засчитается
        var result = _service.MakeMove(1, MoveDirection.Down);

        Assert.AreEqual(1, result.MoveCount);
    }

    // ── IsGameOver ─────────────────────────────────────────────────────────

    [TestMethod]
    public void IsGameOver_BoardFull_NoMerges_ReturnsTrue()
    {
        var game = BuildGame(new[,]
        {
            {  2,  4,  2,  4 },
            {  4,  2,  4,  2 },
            {  2,  4,  2,  4 },
            {  4,  2,  4,  2 },
        });

        _repoMock.Setup(r => r.GetByGameId(1)).Returns(game);

        Assert.IsTrue(_service.IsGameOver(1));
    }

    [TestMethod]
    public void IsGameOver_HasEmptyCell_ReturnsFalse()
    {
        var game = BuildGame(new[,]
        {
            { 2, 4, 2, 4 },
            { 4, 2, 4, 2 },
            { 2, 4, 2, 4 },
            { 4, 2, 0, 2 },
        });

        _repoMock.Setup(r => r.GetByGameId(1)).Returns(game);

        Assert.IsFalse(_service.IsGameOver(1));
    }

    // ── helpers ────────────────────────────────────────────────────────────

    private static GameModel BuildGame(int[,] grid)
    {
        var model = new GameModel { Id = 1, UserId = 1 };
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                model[r, c] = grid[r, c];
        return model;
    }
}
