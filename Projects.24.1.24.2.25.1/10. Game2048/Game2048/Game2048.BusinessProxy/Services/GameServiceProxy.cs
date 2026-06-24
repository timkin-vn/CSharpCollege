using System.Net.Http.Json;
using Game2048.BusinessProxy.Infrastructure;
using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;

namespace Game2048.BusinessProxy.Services;

public class GameServiceProxy : IGameService
{
    public GameModel GetByGameId(int gameId)
    {
        using var http = HttpConnection.CreateClient();
        var reply = http.GetFromJsonAsync<GameReply>($"api/games/get-by-game-id/{gameId}").Result!;
        return FromDto(reply);
    }

    public GameModel GetByUserId(int userId)
    {
        using var http = HttpConnection.CreateClient();
        var reply = http.GetFromJsonAsync<GameReply>($"api/games/get-by-user-id/{userId}").Result!;
        return FromDto(reply);
    }

    public bool? IsGameOver(int gameId)
    {
        using var http = HttpConnection.CreateClient();
        var reply = http.GetFromJsonAsync<BooleanReply>($"api/games/is-over/{gameId}").Result;
        return reply?.IsTrue;
    }

    public bool? IsGameWon(int gameId)
    {
        using var http = HttpConnection.CreateClient();
        var reply = http.GetFromJsonAsync<BooleanReply>($"api/games/is-won/{gameId}").Result;
        return reply?.IsTrue;
    }

    public GameModel MakeMove(int gameId, MoveDirection direction)
    {
        using var http = HttpConnection.CreateClient();
        var response = http.PostAsJsonAsync("api/games/make-move",
            new MakeMoveRequest(gameId, direction.ToString())).Result;
        response.EnsureSuccessStatusCode();
        var reply = response.Content.ReadFromJsonAsync<GameReply>().Result!;
        return FromDto(reply);
    }

    public void RemoveGame(int gameId)
    {
        using var http = HttpConnection.CreateClient();
        http.DeleteAsync($"api/games/remove/{gameId}").Wait();
    }

    // ── helpers ───────────────────────────────────────────────────────────

    private static GameModel FromDto(GameReply dto)
    {
        var model = new GameModel
        {
            Id = dto.Id,
            UserId = dto.UserId,
            MoveCount = dto.MoveCount,
            Score = dto.Score,
            IsWon = dto.IsWon,
        };

        int i = 0;
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                model[r, c] = dto.Cells[i++];

        return model;
    }
}
