using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using Npgsql;
using System.Configuration;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class MazeGameRepository : IMazeGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public MazeGameModel GetByUserId(int userId)
        {
            var selectQuery = @"
SELECT
    ""Id"",
    ""UserId"",
    ""SerializedGameManager""
FROM
    ""MazeGames""
WHERE
    ""UserId"" = @userId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MazeGameModel
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                SerializedGameManager = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public MazeGameModel Save(MazeGameModel mazeGameModel)
        {
            if (mazeGameModel.Id == 0)
            {
                return Create(mazeGameModel);
            }
            return Update(mazeGameModel);
        }

        private MazeGameModel Create(MazeGameModel mazeGameModel)
        {
            var insertQuery = @"
INSERT INTO ""MazeGames"" (""UserId"", ""SerializedGameManager"")
VALUES (@userId, @serializedGameManager)
RETURNING ""Id""";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(insertQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", mazeGameModel.UserId);
                    command.Parameters.AddWithValue("serializedGameManager", mazeGameModel.SerializedGameManager);
                    mazeGameModel.Id = (int)command.ExecuteScalar();
                }
            }
            return mazeGameModel;
        }

        private MazeGameModel Update(MazeGameModel mazeGameModel)
        {
            var updateQuery = @"
UPDATE ""MazeGames""
SET
    ""SerializedGameManager"" = @serializedGameManager
WHERE
    ""Id"" = @id";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(updateQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("serializedGameManager", mazeGameModel.SerializedGameManager);
                    command.Parameters.AddWithValue("id", mazeGameModel.Id);
                    command.ExecuteNonQuery();
                }
            }
            return mazeGameModel;
        }

        public void Remove(int gameId)
        {
            var deleteQuery = @"
DELETE FROM ""MazeGames""
WHERE ""Id"" = @gameId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(deleteQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
