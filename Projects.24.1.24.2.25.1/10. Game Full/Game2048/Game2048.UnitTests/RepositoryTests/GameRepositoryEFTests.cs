using Game2048.DataAccess;
using Game2048.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Game2048.UnitTests.RepositoryTests;

/// <summary>
/// Тесты репозитория на SQLite in-memory базе данных
/// </summary>
[TestClass]
public class GameRepositoryEFTests
{
    private Game2048DbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<Game2048DbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;
        var ctx = new Game2048DbContext(options);
        ctx.Database.EnsureCreated();
        return ctx;
    }

    [TestMethod]
    public void Create_CreatesGameWithTwoTiles()
    {
        using var ctx = CreateContext();
        // Добавим пользователя
        ctx.Users.Add(new DataAccess.Models.UserEntity { Id = 1, Name = "Test" });
        ctx.SaveChanges();

        var repo = new GameRepositoryEF(ctx);
        var game = repo.Create(1);

        Assert.AreEqual(1, game.UserId);
        Assert.AreEqual(0, game.Score);

        // На начальном поле должны быть ровно 2 плитки
        int nonZero = 0;
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                if (game[r, c] != 0) nonZero++;

        Assert.AreEqual(2, nonZero);
    }

    [TestMethod]
    public void GetByUserId_ReturnsLastGame()
    {
        using var ctx = CreateContext();
        ctx.Users.Add(new DataAccess.Models.UserEntity { Id = 1, Name = "Test" });
        ctx.SaveChanges();

        var repo = new GameRepositoryEF(ctx);
        repo.Create(1);
        var second = repo.Create(1);

        var found = repo.GetByUserId(1);

        Assert.IsNotNull(found);
        Assert.AreEqual(second.Id, found.Id);
    }

    [TestMethod]
    public void Update_PersistsScore()
    {
        using var ctx = CreateContext();
        ctx.Users.Add(new DataAccess.Models.UserEntity { Id = 1, Name = "Test" });
        ctx.SaveChanges();

        var repo = new GameRepositoryEF(ctx);
        var game = repo.Create(1);

        game.Score = 512;
        game.MoveCount = 10;
        repo.Update(game);

        var updated = repo.GetByGameId(game.Id);
        Assert.AreEqual(512, updated!.Score);
        Assert.AreEqual(10, updated.MoveCount);
    }

    [TestMethod]
    public void Delete_RemovesGame()
    {
        using var ctx = CreateContext();
        ctx.Users.Add(new DataAccess.Models.UserEntity { Id = 1, Name = "Test" });
        ctx.SaveChanges();

        var repo = new GameRepositoryEF(ctx);
        var game = repo.Create(1);
        repo.Delete(game.Id);

        var deleted = repo.GetByGameId(game.Id);
        Assert.IsNull(deleted);
    }
}
