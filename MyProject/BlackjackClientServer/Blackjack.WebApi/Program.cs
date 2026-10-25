using Blackjack.Business.Services;
using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Contracts.Services;
using Blackjack.DataAccess.DataContext;
using Blackjack.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("В appsettings.json нет строки подключения DefaultConnection.");

// Сервер использует ту же бизнес-логику и доступ к БД, что и проект 9
builder.Services.AddScoped<IUserRepository>(_ => new UserRepository(connectionString));
builder.Services.AddScoped<IGameRepository>(_ => new GameRepository(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DatabaseInitializer.Initialize(connectionString);

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
