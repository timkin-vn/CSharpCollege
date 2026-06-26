using Game2048.BusinessProxy.Services;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;

// ── Сервисы (через BusinessProxy — HTTP-клиент к серверу) ────────────────
IUserService userService = new UserServiceProxy();
IGameService gameService = new GameServiceProxy();

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Title = "Игра 2048";

// ── Вход пользователя ─────────────────────────────────────────────────────
Console.Write("Введите ваше имя: ");
var userName = Console.ReadLine()?.Trim();
if (string.IsNullOrWhiteSpace(userName))
{
    Console.WriteLine("Имя не может быть пустым. Выход.");
    return;
}

UserModel user = userService.GetOrCreateUser(userName);
Console.WriteLine($"\nДобро пожаловать, {user.Name}! (ID={user.Id})");

// ── Загрузка или создание игры ────────────────────────────────────────────
GameModel game = gameService.GetByUserId(user.Id);
Console.WriteLine($"Игра #{game.Id} загружена. Ходов сделано: {game.MoveCount}");
Console.WriteLine("\nУправление: W/A/S/D или ↑←↓→  |  Q — выход\n");

// ── Игровой цикл ──────────────────────────────────────────────────────────
while (true)
{
    DrawBoard(game);

    // Проверяем победу
    if (game.IsWon)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  🎉 Вы достигли 2048! Победа!");
        Console.ResetColor();
        gameService.RemoveGame(game.Id);
        break;
    }

    // Проверяем поражение
    if (gameService.IsGameOver(game.Id) ?? false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ❌ Нет доступных ходов. Игра окончена.");
        Console.ResetColor();
        gameService.RemoveGame(game.Id);
        break;
    }

    var direction = ReadDirection();
    if (direction == null) break; // Q — выход

    game = gameService.MakeMove(game.Id, direction.Value);
}

Console.WriteLine("\nСпасибо за игру! Нажмите любую клавишу для выхода...");
Console.ReadKey(true);

// ── Вспомогательные функции ───────────────────────────────────────────────

static void DrawBoard(GameModel game)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔══════════════════════════════╗");
    Console.WriteLine("║           ИГРА 2048          ║");
    Console.WriteLine("╠══════════════════════════════╣");
    Console.ResetColor();

    Console.WriteLine($"║  Счёт: {game.Score,-8} Ходы: {game.MoveCount,-5}║");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╠══════════════════════════════╣");

    string border = "╠═══════╦═══════╦═══════╦═══════╣";
    string rowSep = "╠═══════╬═══════╬═══════╬═══════╣";
    string bottom = "╚═══════╩═══════╩═══════╩═══════╝";

    Console.ResetColor();
    Console.WriteLine(border);

    for (int r = 0; r < Constants.GridSize; r++)
    {
        // Верхний отступ строки
        Console.Write("║");
        for (int c = 0; c < Constants.GridSize; c++) Console.Write("       ║");
        Console.WriteLine();

        // Значения ячеек
        Console.Write("║");
        for (int c = 0; c < Constants.GridSize; c++)
        {
            int val = game[r, c];
            Console.ForegroundColor = TileColor(val);
            string text = val == 0 ? "  ·  " : val.ToString().PadLeft(3).PadRight(5);
            Console.Write($" {text} ");
            Console.ResetColor();
            Console.Write("║");
        }
        Console.WriteLine();

        // Нижний отступ строки
        Console.Write("║");
        for (int c = 0; c < Constants.GridSize; c++) Console.Write("       ║");
        Console.WriteLine();

        Console.WriteLine(r < Constants.GridSize - 1 ? rowSep : bottom);
    }
    Console.WriteLine();
}

static ConsoleColor TileColor(int value) => value switch
{
    0    => ConsoleColor.DarkGray,
    2    => ConsoleColor.White,
    4    => ConsoleColor.Cyan,
    8    => ConsoleColor.Green,
    16   => ConsoleColor.Yellow,
    32   => ConsoleColor.DarkYellow,
    64   => ConsoleColor.Magenta,
    128  => ConsoleColor.Red,
    256  => ConsoleColor.DarkRed,
    512  => ConsoleColor.Blue,
    1024 => ConsoleColor.DarkCyan,
    2048 => ConsoleColor.DarkGreen,
    _    => ConsoleColor.White,
};

static MoveDirection? ReadDirection()
{
    while (true)
    {
        var key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                return MoveDirection.Up;

            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                return MoveDirection.Down;

            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                return MoveDirection.Left;

            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                return MoveDirection.Right;

            case ConsoleKey.Q:
                return null;

            default:
                continue;
        }
    }
}
