using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;

namespace Game2048.Business.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly Random _rng = new();

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public GameModel GetByGameId(int gameId)
        => _gameRepository.GetByGameId(gameId) ?? throw new InvalidOperationException($"Game {gameId} not found");

    public GameModel GetByUserId(int userId)
        => _gameRepository.GetByUserId(userId) ?? _gameRepository.Create(userId);

    public bool? IsGameOver(int gameId)
    {
        var game = _gameRepository.GetByGameId(gameId);
        return game == null ? null : !CanMove(game);
    }

    public bool? IsGameWon(int gameId)
        => _gameRepository.GetByGameId(gameId)?.IsWon;

    public GameModel MakeMove(int gameId, MoveDirection direction)
    {
        var game = _gameRepository.GetByGameId(gameId);
        if (direction == MoveDirection.None || game == null) return game;

        if (ApplyMove(game, direction))
        {
            game.MoveCount++;
            PlaceRandomTile(game);

            for (int r = 0; r < Constants.GridSize; r++)
            {
                for (int c = 0; c < Constants.GridSize; c++)
                {
                    if (game[r, c] >= Constants.WinValue)
                        game.IsWon = true;
                }
            }

            _gameRepository.Update(game);
        }

        return game;
    }

    public void RemoveGame(int gameId)
        => _gameRepository.Delete(gameId);

    private bool ApplyMove(GameModel game, MoveDirection direction)
    {
        bool moved = false;
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

    private static int[] ExtractLine(GameModel game, MoveDirection dir, int index)
    {
        int[] line = new int[Constants.GridSize];
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

    private static (int row, int col) GetCell(MoveDirection dir, int lineIndex, int pos)
        => dir switch
        {
            MoveDirection.Left => (lineIndex, pos),
            MoveDirection.Right => (lineIndex, Constants.GridSize - 1 - pos),
            MoveDirection.Up => (pos, lineIndex),
            MoveDirection.Down => (Constants.GridSize - 1 - pos, lineIndex),
            _ => throw new ArgumentOutOfRangeException(nameof(dir))
        };

    internal static int[] MergeLine(int[] line, out int score, out bool moved)
    {
        score = 0;
        int[] result = new int[Constants.GridSize];
        int target = 0;

        for (int i = 0; i < Constants.GridSize; i++)
        {
            if (line[i] == 0) continue;

            if (target > 0 && result[target - 1] == line[i])
            {
                result[target - 1] *= 2;
                score += result[target - 1];
                result[target - 1] = -result[target - 1];
            }
            else
            {
                result[target++] = line[i];
            }
        }

        for (int i = 0; i < Constants.GridSize; i++)
        {
            if (result[i] < 0) result[i] = -result[i];
        }

        moved = false;
        for (int i = 0; i < Constants.GridSize; i++)
        {
            if (result[i] != line[i])
            {
                moved = true;
                break;
            }
        }

        return result;
    }

    private void PlaceRandomTile(GameModel game)
    {
        var empty = new List<(int r, int c)>();
        for (int r = 0; r < Constants.GridSize; r++)
        {
            for (int c = 0; c < Constants.GridSize; c++)
            {
                if (game[r, c] == 0) empty.Add((r, c));
            }
        }

        if (empty.Count == 0) return;

        var (row, col) = empty[_rng.Next(empty.Count)];
        game[row, col] = _rng.Next(10) < 9 ? 2 : 4;
    }

    private static bool CanMove(GameModel game)
    {
        for (int r = 0; r < Constants.GridSize; r++)
        {
            for (int c = 0; c < Constants.GridSize; c++)
            {
                if (game[r, c] == 0) return true;
                if (r + 1 < Constants.GridSize && game[r, c] == game[r + 1, c]) return true;
                if (c + 1 < Constants.GridSize && game[r, c] == game[r, c + 1]) return true;
            }
        }
        return false;
    }
}