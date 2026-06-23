using System;
using System.Collections.Generic;
using System.Configuration;
using SeaBattle.Common;
using SeaBattle.Common.Interfaces;
using SeaBattle.Common.Models;
using Npgsql;

namespace SeaBattle.DataAccess
{
    public class GameRepositoryAdo : IGameRepository
    {
        private static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString; }
        }

        public GameModel Save(GameModel game)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO public.\"Games\" (\"UserId\",\"MoveCount\",\"Won\") " +
                    "VALUES (@uid,@mc,@won) RETURNING \"Id\"", conn))
                {
                    cmd.Parameters.AddWithValue("uid", game.UserId);
                    cmd.Parameters.AddWithValue("mc", game.MoveCount);
                    cmd.Parameters.AddWithValue("won", game.Won);
                    game.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return game;
        }

        public IList<GameModel> GetByUserId(int userId)
        {
            var list = new List<GameModel>();
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "SELECT \"Id\",\"UserId\",\"MoveCount\",\"Won\" FROM public.\"Games\" " +
                    "WHERE \"UserId\"=@uid ORDER BY \"Id\" DESC", conn))
                {
                    cmd.Parameters.AddWithValue("uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(new GameModel
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                MoveCount = reader.GetInt32(2),
                                Won = reader.GetBoolean(3)
                            });
                    }
                }
            }
            return list;
        }

        public void Remove(int gameId)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Games\" WHERE \"Id\"=@id", conn))
                {
                    cmd.Parameters.AddWithValue("id", gameId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
