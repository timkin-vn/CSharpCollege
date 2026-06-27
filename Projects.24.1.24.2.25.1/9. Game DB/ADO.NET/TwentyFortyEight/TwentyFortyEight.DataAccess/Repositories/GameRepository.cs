using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Npgsql;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Definitions;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _dbConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public GameDto GetByGameId(int gameId)
        {
            const string queryText = @"
                SELECT g.""Id"", g.""UserId"", g.""Score"", g.""BestTile"", g.""IsWon"", 
                       c.""Row"", c.""Column"", c.""Value""
                FROM ""Games"" g
                INNER JOIN ""Cells"" c ON g.""Id"" = c.""GameId""
                WHERE g.""Id"" = @gameId";

            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();
                GameDto mappedGame = null;

                using (var dbCommand = new NpgsqlCommand(queryText, dbConnection))
                {
                    dbCommand.Parameters.AddWithValue("gameId", gameId);

                    using (var dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (mappedGame == null)
                            {
                                mappedGame = new GameDto
                                {
                                    Id = dataReader.GetInt32(0),
                                    UserId = dataReader.GetInt32(1),
                                    Score = dataReader.GetInt32(2),
                                    BestTile = dataReader.GetInt32(3),
                                    IsWon = dataReader.GetBoolean(4)
                                };
                            }

                            mappedGame.Cells[dataReader.GetInt32(5), dataReader.GetInt32(6)] = dataReader.GetInt32(7);
                        }
                    }
                }

                return mappedGame;
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            const string queryText = @"
                SELECT g.""Id"", g.""UserId"", g.""Score"", g.""BestTile"", g.""IsWon"", 
                       c.""Row"", c.""Column"", c.""Value""
                FROM ""Games"" g
                INNER JOIN ""Cells"" c ON g.""Id"" = c.""GameId""
                WHERE g.""UserId"" = @userId
                ORDER BY g.""Id""";

            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();
                var collectedGames = new List<GameDto>();

                using (var dbCommand = new NpgsqlCommand(queryText, dbConnection))
                {
                    dbCommand.Parameters.AddWithValue("userId", userId);

                    using (var dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int activeId = dataReader.GetInt32(0);
                            var matchedDto = collectedGames.FirstOrDefault(x => x.Id == activeId);

                            if (matchedDto == null)
                            {
                                matchedDto = new GameDto
                                {
                                    Id = activeId,
                                    UserId = dataReader.GetInt32(1),
                                    Score = dataReader.GetInt32(2),
                                    BestTile = dataReader.GetInt32(3),
                                    IsWon = dataReader.GetBoolean(4)
                                };
                                collectedGames.Add(matchedDto);
                            }

                            matchedDto.Cells[dataReader.GetInt32(5), dataReader.GetInt32(6)] = dataReader.GetInt32(7);
                        }
                    }
                }

                return collectedGames;
            }
        }

        public void Remove(int gameId)
        {
            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();

                using (var cleanCellsCmd = new NpgsqlCommand(@"DELETE FROM ""Cells"" WHERE ""GameId"" = @gameId", dbConnection))
                {
                    cleanCellsCmd.Parameters.AddWithValue("gameId", gameId);
                    cleanCellsCmd.ExecuteNonQuery();
                }

                using (var cleanGameCmd = new NpgsqlCommand(@"DELETE FROM ""Games"" WHERE ""Id"" = @gameId", dbConnection))
                {
                    cleanGameCmd.Parameters.AddWithValue("gameId", gameId);
                    cleanGameCmd.ExecuteNonQuery();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            return gameDto.Id == 0 ? InsertRecord(gameDto) : UpdateRecord(gameDto);
        }

        private int UpdateRecord(GameDto dto)
        {
            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();

                using (var modifyGameCmd = new NpgsqlCommand(@"
                    UPDATE ""Games""
                    SET ""Score"" = @score, ""BestTile"" = @bestTile, ""IsWon"" = @isWon
                    WHERE ""Id"" = @gameId", dbConnection))
                {
                    modifyGameCmd.Parameters.AddWithValue("gameId", dto.Id);
                    modifyGameCmd.Parameters.AddWithValue("score", dto.Score);
                    modifyGameCmd.Parameters.AddWithValue("bestTile", dto.BestTile);
                    modifyGameCmd.Parameters.AddWithValue("isWon", dto.IsWon);
                    modifyGameCmd.ExecuteNonQuery();
                }

                using (var purgeCellsCmd = new NpgsqlCommand(@"DELETE FROM ""Cells"" WHERE ""GameId"" = @gameId", dbConnection))
                {
                    purgeCellsCmd.Parameters.AddWithValue("gameId", dto.Id);
                    purgeCellsCmd.ExecuteNonQuery();
                }

                for (int rowIndex = 0; rowIndex < Constants.RowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < Constants.ColumnCount; colIndex++)
                    {
                        int currentCellValue = dto.Cells[rowIndex, colIndex];
                        if (currentCellValue == 0) continue;

                        using (var writeCellCmd = new NpgsqlCommand(@"
                            INSERT INTO ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
                            VALUES (@gameId, @row, @column, @value)", dbConnection))
                        {
                            writeCellCmd.Parameters.AddWithValue("gameId", dto.Id);
                            writeCellCmd.Parameters.AddWithValue("row", rowIndex);
                            writeCellCmd.Parameters.AddWithValue("column", colIndex);
                            writeCellCmd.Parameters.AddWithValue("value", currentCellValue);
                            writeCellCmd.ExecuteNonQuery();
                        }
                    }
                }

                return dto.Id;
            }
        }

        private int InsertRecord(GameDto dto)
        {
            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();
                int generatedGameId;

                using (var appendGameCmd = new NpgsqlCommand(@"
                    INSERT INTO ""Games"" (""UserId"", ""Score"", ""BestTile"", ""IsWon"")
                    VALUES (@userId, @score, @bestTile, @isWon)
                    RETURNING ""Id""", dbConnection))
                {
                    appendGameCmd.Parameters.AddWithValue("userId", dto.UserId);
                    appendGameCmd.Parameters.AddWithValue("score", dto.Score);
                    appendGameCmd.Parameters.AddWithValue("bestTile", dto.BestTile);
                    appendGameCmd.Parameters.AddWithValue("isWon", dto.IsWon);
                    generatedGameId = (int)appendGameCmd.ExecuteScalar();
                }

                for (int rowIndex = 0; rowIndex < Constants.RowCount; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < Constants.ColumnCount; colIndex++)
                    {
                        int currentCellValue = dto.Cells[rowIndex, colIndex];
                        if (currentCellValue == 0) continue;

                        using (var appendCellCmd = new NpgsqlCommand(@"
                            INSERT INTO ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
                            VALUES (@gameId, @row, @column, @value)", dbConnection))
                        {
                            appendCellCmd.Parameters.AddWithValue("gameId", generatedGameId);
                            appendCellCmd.Parameters.AddWithValue("row", rowIndex);
                            appendCellCmd.Parameters.AddWithValue("column", colIndex);
                            appendCellCmd.Parameters.AddWithValue("value", currentCellValue);
                            appendCellCmd.ExecuteNonQuery();
                        }
                    }
                }

                return generatedGameId;
            }
        }
    }
}