using GameDB.DataAccess.Interfaces;
using GameDB.DataAccess.Repositories;

namespace GameDB.DataAccess;

public class UnitOfWork : IDisposable
{
    private readonly string _connectionString;
    private bool _disposed;

    public UnitOfWork(string connectionString)
    {
        _connectionString = connectionString;
        Players = new PlayerRepository(connectionString);
        Leaderboards = new LeaderboardRepository(connectionString);
        Sessions = new SessionRepository(connectionString);
    }

    public IPlayerRepository Players { get; }
    public ILeaderboardRepository Leaderboards { get; }
    public ISessionRepository Sessions { get; }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }
}