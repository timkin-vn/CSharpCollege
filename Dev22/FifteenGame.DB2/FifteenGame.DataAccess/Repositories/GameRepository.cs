using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {

        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MoveCount"",
    g.""GameBegin"",
    c.""Row"",
    c.""Column"",
    c.""IsMine"",
    c.""NeightborMines"",
    c.""IsRevealed"",
    c.""Isflag""
from 
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where
    g.""Id"" = @gameId
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                GameDto result = null;
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (result == null)
                            {
                                result = new GameDto
                                {
                                    Id = reader.GetInt32(0),
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2),
                                    GameBegin = reader.GetDateTime(3),
                                    Cells = new CellsModel[Constants.RowCount, Constants.ColumnCount] // Используем CellsModel
                                };

                                // Инициализация ячеек
                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = new CellsModel();
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(4);
                            var columnVal = reader.GetInt32(5);
                            var isMine = reader.GetBoolean(6);
                            var neighborMines = reader.GetInt32(7);
                            var isRevealed = reader.GetBoolean(8);
                            var isFlag = reader.GetBoolean(9);

                            // Устанавливаем значения ячейки
                            result.Cells[rowVal, columnVal].IsMine = isMine;
                            result.Cells[rowVal, columnVal].NeightborMines = neighborMines;
                            result.Cells[rowVal, columnVal].IsRevealed = isRevealed;
                            result.Cells[rowVal, columnVal].Isflag = isFlag;
                        }
                    }
                }

                return result;
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MoveCount"",
    g.""GameBegin"",
    c.""Row"",
    c.""Column"",
    c.""IsMine"",
    c.""NeightborMines"",
    c.""IsRevealed"",
    c.""Isflag""
from 
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where
    g.""UserId"" = @userId
order by
    g.""Id""
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                List<GameDto> result = new List<GameDto>();
                connection.Open();


                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        GameDto gameDto = null;

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);

                            gameDto = result.FirstOrDefault(g => g.Id == id);
                            if (gameDto == null)
                            {
                                gameDto = new GameDto
                                {
                                    Id = id,
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2),
                                    GameBegin = reader.GetDateTime(3),
                                    Cells = new CellsModel[Constants.RowCount, Constants.ColumnCount] // Используем CellsModel
                                };

                                // Инициализация ячеек
                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = new CellsModel(); // Инициализация ячеек
                                    }
                                }
                                result.Add(gameDto);
                            }

                            var rowVal = reader.GetInt32(4);
                            var columnVal = reader.GetInt32(5);
                            var isMine = reader.GetBoolean(6);
                            var neighborMines = reader.GetInt32(7);
                            var isRevealed = reader.GetBoolean(8);
                            var isFlag = reader.GetBoolean(9);

                            // Устанавливаем значения ячейки
                            gameDto.Cells[rowVal, columnVal].IsMine = isMine;
                            gameDto.Cells[rowVal, columnVal].NeightborMines = neighborMines;
                            gameDto.Cells[rowVal, columnVal].IsRevealed = isRevealed;
                            gameDto.Cells[rowVal, columnVal].Isflag = isFlag;
                        }
                    }
                }

                return result;
            }
        }

        public void Remove(int gameId)
        {
            var removeCellsQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";
            var removeGameQuery = @"
delete from ""Games""
where ""Id"" = @gameId
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(removeCellsQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(removeGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
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
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"", ""GameBegin"")
values (@userId, @moveCount, @gameBegin)
returning ""Id""
";
            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""IsMine"", ""NeightborMines"", ""IsRevealed"", ""Isflag"")
values (@gameId, @row, @column, @isMine, @neighborMines, @isRevealed, @isFlag)
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId;
                connection.Open();

                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("gameBegin", gameDto.GameBegin);

                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var cell = gameDto.Cells[row, column];

                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.Text;
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.Parameters.AddWithValue("isMine", cell.IsMine);
                            command.Parameters.AddWithValue("neighborMines", cell.NeightborMines);
                            command.Parameters.AddWithValue("isRevealed", cell.IsRevealed);
                            command.Parameters.AddWithValue("isFlag", cell.Isflag);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                return gameId;
            }
        }

        private int Update(GameDto gameDto)
        {
            var updateGameQuery = @"
update ""Games""
set
    ""MoveCount"" = @moveCount
where
    ""Id"" = @gameId
";

            var removeCellsQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";
            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""IsMine"", ""NeightborMines"", ""IsRevealed"", ""Isflag"")
values (@gameId, @row, @column, @isMine, @neighborMines, @isRevealed, @isFlag)
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId = gameDto.Id;
                connection.Open();

                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);

                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(removeCellsQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    command.ExecuteNonQuery();
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var cell = gameDto.Cells[row, column];

                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.Text;
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.Parameters.AddWithValue("isMine", cell.IsMine);
                            command.Parameters.AddWithValue("neighborMines", cell.NeightborMines);
                            command.Parameters.AddWithValue("isRevealed", cell.IsRevealed);
                            command.Parameters.AddWithValue("isFlag", cell.Isflag);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                return gameId;
            }
        }

    }
}
