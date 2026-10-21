
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class GameRepositoryEF : IGameRepository
    {
        public GameDto GetByGameId(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameId);

                return ToDto(game);
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                return context.Games
                    .Include("Cells")
                    .Where(g => g.UserId == userId)
                    .OrderBy(g => g.Id)
                    .ToList()
                    .Select(ToDto)
                    .ToList();
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameId);

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
                    throw new InvalidOperationException("Пользователь не найден.");
                }

                var game = new Game
                {
                    UserId = user.Id,
                    MoveCount = gameDto.MoveCount,
                    Score = gameDto.Score,
                    BestScore = gameDto.BestScore,
                    Cells = new List<Cell>()
                };

                for (int row = 0; row < 4; row++)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        game.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = gameDto.Cells[row, column]
                        });
                    }
                }

                context.Games.Add(game);
                context.SaveChanges();

                return game.Id;
            }
        }

        private int Update(GameDto gameDto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == gameDto.Id);

                if (game == null)
                {
                    throw new InvalidOperationException("Игра не найдена.");
                }

                game.MoveCount = gameDto.MoveCount;
                game.Score = gameDto.Score;
                game.BestScore = gameDto.BestScore;

                for (int row = 0; row < 4; row++)
                {
                    for (int column = 0; column < 4; column++)
                    {
                        var cell = game.Cells.FirstOrDefault(c =>
                            c.Row == row && c.Column == column);

                        if (cell == null)
                        {
                            game.Cells.Add(new Cell
                            {
                                Row = row,
                                Column = column,
                                Value = gameDto.Cells[row, column]
                            });
                        }
                        else
                        {
                            cell.Value = gameDto.Cells[row, column];
                        }
                    }
                }

                context.SaveChanges();
                return game.Id;
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
                MoveCount = game.MoveCount,
                Score = game.Score,
                BestScore = game.BestScore
            };

            foreach (var cell in game.Cells)
            {
                if (cell.Row >= 0 && cell.Row < 4 &&
                    cell.Column >= 0 && cell.Column < 4)
                {
                    result.Cells[cell.Row, cell.Column] = cell.Value;
                }
            }

            return result;
        }
    }
}
