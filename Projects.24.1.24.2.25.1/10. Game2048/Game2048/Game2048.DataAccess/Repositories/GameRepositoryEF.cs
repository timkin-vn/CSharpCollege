using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Definitions;
using Game2048.DataAccess.Models;

namespace Game2048.DataAccess.Repositories;

public class GameRepositoryEF : IGameRepository
{
    private readonly Game2048DbContext _db;

    public GameRepositoryEF(Game2048DbContext db)
    {
        _db = db;
    }

    public GameModel? GetByGameId(int gameId)
    {
        var entity = _db.Games.FirstOrDefault(g => g.Id == gameId);
        return entity == null ? null : ToModel(entity);
    }

    public GameModel? GetByUserId(int userId)
    {
        // Возвращаем последнюю незавершённую игру
        var entity = _db.Games
            .Where(g => g.UserId == userId && !g.IsWon)
            .OrderByDescending(g => g.Id)
            .FirstOrDefault();
        return entity == null ? null : ToModel(entity);
    }

    public GameModel Create(int userId)
    {
        var grid = GenerateInitialGrid();
        var entity = new GameEntity
        {
            UserId = userId,
            MoveCount = 0,
            Score = 0,
            IsWon = false,
            GridData = GridToString(grid),
        };
        _db.Games.Add(entity);
        _db.SaveChanges();
        return ToModel(entity);
    }

    public GameModel Update(GameModel game)
    {
        var entity = _db.Games.Find(game.Id)
            ?? throw new InvalidOperationException($"Game {game.Id} not found");

        entity.MoveCount = game.MoveCount;
        entity.Score = game.Score;
        entity.IsWon = game.IsWon;
        entity.GridData = ModelToGridString(game);
        _db.SaveChanges();
        return ToModel(entity);
    }

    public void Delete(int gameId)
    {
        var entity = _db.Games.Find(gameId);
        if (entity != null)
        {
            _db.Games.Remove(entity);
            _db.SaveChanges();
        }
    }

    // ── helpers ────────────────────────────────────────────────────────────

    private static int[,] GenerateInitialGrid()
    {
        var grid = new int[Constants.GridSize, Constants.GridSize];
        var rng = new Random();
        // Размещаем две начальные плитки: 2 или 4
        PlaceRandomTile(grid, rng);
        PlaceRandomTile(grid, rng);
        return grid;
    }

    internal static void PlaceRandomTile(int[,] grid, Random rng)
    {
        var empty = new List<(int r, int c)>();
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                if (grid[r, c] == 0) empty.Add((r, c));

        if (empty.Count == 0) return;

        var (row, col) = empty[rng.Next(empty.Count)];
        grid[row, col] = rng.Next(10) < 9 ? 2 : 4; // 90% шанс на «2», 10% на «4»
    }

    private static string GridToString(int[,] grid)
    {
        var values = new List<int>();
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                values.Add(grid[r, c]);
        return string.Join(",", values);
    }

    private static string ModelToGridString(GameModel model)
    {
        var values = new List<int>();
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                values.Add(model[r, c]);
        return string.Join(",", values);
    }

    private static GameModel ToModel(GameEntity entity)
    {
        var model = new GameModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            MoveCount = entity.MoveCount,
            Score = entity.Score,
            IsWon = entity.IsWon,
        };

        var values = entity.GridData.Split(',').Select(int.Parse).ToArray();
        int i = 0;
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                model[r, c] = values[i++];

        return model;
    }
}
