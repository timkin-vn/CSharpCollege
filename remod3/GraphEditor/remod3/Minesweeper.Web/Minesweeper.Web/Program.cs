using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Web.Models;
using Minesweeper.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<JsonOptions>(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddScoped<GameService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();

var api = app.MapGroup("/api/game");
api.MapGet("/", (GameService service) => Results.Ok(service.GetView()));
api.MapPost("/new", ([FromBody] DifficultyRequest request, GameService service) => Results.Ok(service.StartNewGame(request)));
api.MapPost("/action", ([FromBody] GameActionRequest request, GameService service) => Results.Ok(service.ApplyAction(request)));

app.Run();
