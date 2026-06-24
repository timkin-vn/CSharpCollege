using Game2048.Business.Services;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Contracts.Services;
using Game2048.DataAccess;
using Game2048.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Регистрация зависимостей (аналог NinjectModule из FifteenGame) ────────
builder.Services.AddDbContext<Game2048DbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=game2048.db"));

builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();
builder.Services.AddScoped<IGameRepository, GameRepositoryEF>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Game 2048 API", Version = "v1" });
});

var app = builder.Build();

// ── Автоматическое создание / миграция БД при старте ─────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Game2048DbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
