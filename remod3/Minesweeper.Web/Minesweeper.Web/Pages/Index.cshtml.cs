using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Minesweeper.Web.Models;
using Minesweeper.Web.Services;

namespace Minesweeper.Web.Pages;

public sealed class IndexModel : PageModel {
    private readonly GameService _service;

    public IndexModel(GameService service) {
        _service = service;
    }

    public GameViewModel View { get; private set; } = default!;
    public string InitialJson { get; private set; } = string.Empty;

    public void OnGet() {
        View = _service.GetView();
        InitialJson = JsonSerializer.Serialize(View, new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });
    }
}
