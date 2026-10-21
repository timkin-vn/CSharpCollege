
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
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

        private int Create(GameDto dto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == dto.UserId);

                if (user == null)
                {
                    throw new Exception("Пользователь не найден.");
                }

                var game = new Game
                {
                    UserId = dto.UserId,
                    MoveCount = dto.MoveCount,
                    Score = dto.Score,
                    BestScore = dto.BestScore,
                    Cells = new List<Cell>()
                };

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        game.Cells.Add(new Cell
                        {
                            Row = row,
                            Column = column,
                            Value = dto.Cells[row, column]
                        });
                    }
                }

                context.Games.Add(game);
                context.SaveChanges();

                return game.Id;
            }
        }

        private int Update(GameDto dto)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.Games
                    .Include("Cells")
                    .FirstOrDefault(g => g.Id == dto.Id);

                if (game == null)
                {
                    throw new Exception("Игра не найдена.");
                }

                game.MoveCount = dto.MoveCount;
                game.Score = dto.Score;
                game.BestScore = Math.Max(game.BestScore, dto.BestScore);

                context.Cells.RemoveRange(game.Cells.ToList());
                context.SaveChanges();

                game.Cells = new List<Cell>();

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        game.Cells.Add(new Cell
                        {
                            GameId = game.Id,
                            Row = row,
                            Column = column,
                            Value = dto.Cells[row, column]
                        });
                    }
                }

                context.Cells.AddRange(game.Cells);
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

            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                Score = game.Score,
                BestScore = game.BestScore
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = 0;
                }
            }

            foreach (var cell in game.Cells)
            {
                dto.Cells[cell.Row, cell.Column] = cell.Value;
            }

            return dto;
        }
    }
}
