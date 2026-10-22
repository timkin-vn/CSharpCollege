namespace Game2048.Common.BusinessDtos;

// ── Запросы ──────────────────────────────────────────────────
public record UserNameRequest(string UserName);

public record MakeMoveRequest(int GameId, string Direction);

// ── Ответы ───────────────────────────────────────────────────
public class UserReply
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UsersReply
{
    public List<UserReply> Users { get; set; } = [];
}

public class GameReply
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MoveCount { get; set; }
    public int Score { get; set; }
    public bool IsWon { get; set; }

    /// <summary>
    /// Плоский массив ячеек: строка за строкой, GridSize*GridSize элементов
    /// </summary>
    public int[] Cells { get; set; } = [];
}

public class BooleanReply
{
    public bool IsTrue { get; set; }
}
