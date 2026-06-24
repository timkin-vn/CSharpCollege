using GameDB.DataAccess.Interfaces;
using GameDB.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameDB.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<DatabaseInitializer>(_ => new DatabaseInitializer(connectionString));
        services.AddScoped<IPlayerRepository>(_ => new PlayerRepository(connectionString));
        services.AddScoped<ILeaderboardRepository>(_ => new LeaderboardRepository(connectionString));
        services.AddScoped<ISessionRepository>(_ => new SessionRepository(connectionString));
        services.AddScoped<UnitOfWork>(_ => new UnitOfWork(connectionString));

        return services;
    }
}