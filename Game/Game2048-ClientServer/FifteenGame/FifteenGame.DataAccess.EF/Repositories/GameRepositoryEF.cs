using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var games = context.Games.Include("GameCells").Where(g => g.UserId == userId);
                return games.Select(ToDto).ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games.Include("GameCells").FirstOrDefault(g => g.Id == gameId);
                if (game == null)
                {
                    return;
                }

                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                var newGame = new Game
                {
                    User = user,
                    Score = gameDto.Score,
                    IsGameOver = gameDto.IsGameOver,
                    HasWon = gameDto.HasWon,
                    GameCells = new List<GameCell>(),
                };

                context.Games.Add(newGame);

                // Save all cells including zeros
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        int index = row * Constants.ColumnCount + column;
                        newGame.GameCells.Add(new GameCell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[index],
                        });
                    }
                }

                context.SaveChanges();
                return newGame.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                try
                {
                    // Проверяем, существует ли игра
                    var existingGame = context.Games.FirstOrDefault(g => g.Id == gameDto.Id);
                    if (existingGame == null)
                    {
                        // Если игра не существует, создаем новую с тем же ID
                        return CreateWithId(gameDto);
                    }

                    // Используем сырой SQL для обновления игры
                    context.Database.ExecuteSqlCommand(
                        "UPDATE Games SET Score = @p0, IsGameOver = @p1, HasWon = @p2 WHERE Id = @p3",
                        gameDto.Score, gameDto.IsGameOver, gameDto.HasWon, gameDto.Id);

                    // Удаляем старые ячейки
                    context.Database.ExecuteSqlCommand(
                        "DELETE FROM GameCells WHERE GameId = @p0", gameDto.Id);

                    // Вставляем новые ячейки
                    for (int row = 0; row < Constants.RowCount; row++)
                    {
                        for (int column = 0; column < Constants.ColumnCount; column++)
                        {
                            int index = row * Constants.ColumnCount + column;
                            context.Database.ExecuteSqlCommand(
                                "INSERT INTO GameCells (GameId, [Row], [Column], Value) VALUES (@p0, @p1, @p2, @p3)",
                                gameDto.Id, row, column, gameDto.Cells[index]);
                        }
                    }

                    return gameDto.Id;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private int CreateWithId(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == gameDto.UserId);
                if (user == null)
                {
                    throw new Exception("Нет такого пользователя");
                }

                // Вставляем игру с указанным ID используя сырой SQL
                context.Database.ExecuteSqlCommand(
                    "INSERT INTO Games (Id, UserId, Score, IsGameOver, HasWon) VALUES (@p0, @p1, @p2, @p3, @p4)",
                    gameDto.Id, gameDto.UserId, gameDto.Score, gameDto.IsGameOver, gameDto.HasWon);

                // Вставляем ячейки
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        int index = row * Constants.ColumnCount + column;
                        context.Database.ExecuteSqlCommand(
                            "INSERT INTO GameCells (GameId, [Row], [Column], Value) VALUES (@p0, @p1, @p2, @p3)",
                            gameDto.Id, row, column, gameDto.Cells[index]);
                    }
                }

                return gameDto.Id;
            }
        }

        private GameDto ToDto(Game game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Score = game.Score,
                IsGameOver = game.IsGameOver,
                HasWon = game.HasWon,
                Cells = new int[Constants.RowCount * Constants.ColumnCount] // Явная инициализация массива
            };

            // Initialize all cells with zeros
            for (int i = 0; i < Constants.RowCount * Constants.ColumnCount; i++)
            {
                result.Cells[i] = Constants.FreeCellValue;
            }

            // Fill with actual values
            if (game.GameCells != null)
            {
                foreach (var cell in game.GameCells)
                {
                    int index = cell.Row * Constants.ColumnCount + cell.Column;
                    result.Cells[index] = cell.Value;
                }
            }

            return result;
        }
    }
}
