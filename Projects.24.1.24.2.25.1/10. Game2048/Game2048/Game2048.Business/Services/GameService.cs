using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;

namespace Game2048.Business.Services;

/// <summary>
/// Бизнес-логика игры 2048.
/// Отвечает за ходы, слияние плиток и проверку условий победы/поражения.
/// </summary>
public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly Random _rng = new();

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public GameModel GetByGameId(int gameId)
    => _gameRepository.GetByGameId(gameId)
       ?? throw new InvalidOperationException($"Game {gameId} not found");


    /// <summary>
    /// Возвращает активную игру пользователя или создаёт новую.
    /// </summary>
    public GameModel GetByUserId(int userId)
        => _gameRepository.GetByUserId(userId) ?? _gameRepository.Create(userId);

    public bool? IsGameOver(int gameId)
    {
        var game = _gameRepository.GetByGameId(gameId);
        if (game == null) return null;
        return !CanMove(game);
    }

    public bool? IsGameWon(int gameId)
    {
        var game = _gameRepository.GetByGameId(gameId);
        if (game == null) return null;
        return game.IsWon;
    }

    public GameModel MakeMove(int gameId, MoveDirection direction)
    {
        var game = _gameRepository.GetByGameId(gameId);

        if (direction == MoveDirection.None) return game;

        bool moved = ApplyMove(game, direction);

        if (moved)
        {
            game.MoveCount++;
            PlaceRandomTile(game);

            // Проверяем победу
            for (int r = 0; r < Constants.GridSize; r++)
                for (int c = 0; c < Constants.GridSize; c++)
                    if (game[r, c] >= Constants.WinValue)
                        game.IsWon = true;

            _gameRepository.Update(game);
        }

        return game;
    }

    public void RemoveGame(int gameId)
        => _gameRepository.Delete(gameId);

    // ── Алгоритм хода ─────────────────────────────────────────────────────

    private bool ApplyMove(GameModel game, MoveDirection direction)
    {
        bool moved = false;

        // Перебираем строки/столбцы в правильном порядке в зависимости от направления
        for (int i = 0; i < Constants.GridSize; i++)
        {
            int[] line = ExtractLine(game, direction, i);
            int[] merged = MergeLine(line, out int score, out bool lineMoved);
            if (lineMoved) moved = true;
            game.Score += score;
            PutLine(game, direction, i, merged);
        }

        return moved;
    }

    /// <summary>
    /// Извлекает «линию» (строку или столбец) в направлении хода.
    /// Для Up/Left — порядок 0→3, для Down/Right — 3→0 (чтобы слияние шло в нужную сторону).
    /// </summary>
    private static int[] ExtractLine(GameModel game, MoveDirection dir, int index)
    {
        var line = new int[Constants.GridSize];
        for (int k = 0; k < Constants.GridSize; k++)
        {
            (int r, int c) = GetCell(dir, index, k);
            line[k] = game[r, c];
        }
        return line;
    }

    private static void PutLine(GameModel game, MoveDirection dir, int index, int[] line)
    {
        for (int k = 0; k < Constants.GridSize; k++)
        {
            (int r, int c) = GetCell(dir, index, k);
            game[r, c] = line[k];
        }
    }

    /// <summary>
    /// Преобразует (направление, индекс линии, позиция в линии) → (row, col) в массиве.
    /// </summary>
    private static (int row, int col) GetCell(MoveDirection dir, int lineIndex, int pos)
        => dir switch
        {
            MoveDirection.Left  => (lineIndex, pos),
            MoveDirection.Right => (lineIndex, Constants.GridSize - 1 - pos),
            MoveDirection.Up    => (pos, lineIndex),
            MoveDirection.Down  => (Constants.GridSize - 1 - pos, lineIndex),
            _ => throw new ArgumentOutOfRangeException(nameof(dir))
        };

    /// <summary>
    /// Основная механика 2048: сдвигаем все ненулевые влево, затем сливаем соседние равные.
    /// </summary>
    internal static int[] MergeLine(int[] line, out int score, out bool moved)
    {
        score = 0;
        var result = new int[Constants.GridSize];

        // 1. Сжимаем: убираем нули, оставляя только ненулевые значения
        var nonZero = line.Where(x => x != 0).ToArray();

        // 2. Слияние соседних равных
        var merged = new List<int>();
        for (int i = 0; i < nonZero.Length; i++)
        {
            if (i + 1 < nonZero.Length && nonZero[i] == nonZero[i + 1])
            {
                int mergedValue = nonZero[i] * 2;
                merged.Add(mergedValue);
                score += mergedValue;
                i++; // пропускаем следующий элемент, он уже слился
            }
            else
            {
                merged.Add(nonZero[i]);
            }
        }

        // 3. Копируем в результат (остаток заполняется нулями)
        for (int i = 0; i < merged.Count; i++)
            result[i] = merged[i];

        moved = !result.SequenceEqual(line);
        return result;
    }

    /// <summary>
    /// Размещает случайную новую плитку (2 или 4) на пустой клетке.
    /// </summary>
    private void PlaceRandomTile(GameModel game)
    {
        var empty = new List<(int r, int c)>();
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
                if (game[r, c] == 0) empty.Add((r, c));

        if (empty.Count == 0) return;

        var (row, col) = empty[_rng.Next(empty.Count)];
        game[row, col] = _rng.Next(10) < 9 ? 2 : 4;
    }

    /// <summary>
    /// Проверяет, есть ли хоть один возможный ход (пустые клетки или соседние равные).
    /// </summary>
    private static bool CanMove(GameModel game)
    {
        for (int r = 0; r < Constants.GridSize; r++)
            for (int c = 0; c < Constants.GridSize; c++)
            {
                if (game[r, c] == 0) return true;
                if (r + 1 < Constants.GridSize && game[r, c] == game[r + 1, c]) return true;
                if (c + 1 < Constants.GridSize && game[r, c] == game[r, c + 1]) return true;
            }
        return false;
    }
}
