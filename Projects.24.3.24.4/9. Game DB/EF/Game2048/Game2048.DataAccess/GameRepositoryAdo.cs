using System;
using System.Configuration;
using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;
using Npgsql;

namespace Game2048.DataAccess
{

    public class GameRepositoryAdo : IGameRepository
    {
        private static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString; }
        }

        public GameModel GetByUserId(int userId)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                GameModel game = null;

                using (var cmd = new NpgsqlCommand(
                    "SELECT \"Id\",\"UserId\",\"Score\",\"MoveCount\" FROM public.\"Games\" " +
                    "WHERE \"UserId\"=@uid ORDER BY \"Id\" DESC LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            game = new GameModel
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Score = reader.GetInt32(2),
                                MoveCount = reader.GetInt32(3)
                            };
                    }
                }

                if (game == null) return null;

                using (var cmd = new NpgsqlCommand(
                    "SELECT \"Row\",\"Column\",\"Value\" FROM public.\"Cells\" WHERE \"GameId\"=@gid", conn))
                {
                    cmd.Parameters.AddWithValue("gid", game.Id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int row = reader.GetInt32(0);
                            int col = reader.GetInt32(1);
                            int val = reader.GetInt32(2);
                            if (row >= 0 && row < Constants.Size && col >= 0 && col < Constants.Size)
                                game.Field[row, col] = val;
                        }
                    }
                }
                return game;
            }
        }

        public void SaveGame(GameModel game)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    if (game.Id == 0)
                    {
                        using (var cmd = new NpgsqlCommand(
                            "INSERT INTO public.\"Games\" (\"UserId\",\"Score\",\"MoveCount\") " +
                            "VALUES (@uid,@score,@mc) RETURNING \"Id\"", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("uid", game.UserId);
                            cmd.Parameters.AddWithValue("score", game.Score);
                            cmd.Parameters.AddWithValue("mc", game.MoveCount);
                            game.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        for (int r = 0; r < Constants.Size; r++)
                            for (int c = 0; c < Constants.Size; c++)
                            {
                                using (var cmd = new NpgsqlCommand(
                                    "INSERT INTO public.\"Cells\" (\"GameId\",\"Row\",\"Column\",\"Value\") " +
                                    "VALUES (@gid,@row,@col,@val)", conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("gid", game.Id);
                                    cmd.Parameters.AddWithValue("row", r);
                                    cmd.Parameters.AddWithValue("col", c);
                                    cmd.Parameters.AddWithValue("val", game.Field[r, c]);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                    }
                    else
                    {
                        using (var cmd = new NpgsqlCommand(
                            "UPDATE public.\"Games\" SET \"Score\"=@score,\"MoveCount\"=@mc WHERE \"Id\"=@id", conn, tx))
                        {
                            cmd.Parameters.AddWithValue("score", game.Score);
                            cmd.Parameters.AddWithValue("mc", game.MoveCount);
                            cmd.Parameters.AddWithValue("id", game.Id);
                            cmd.ExecuteNonQuery();
                        }

                        for (int r = 0; r < Constants.Size; r++)
                            for (int c = 0; c < Constants.Size; c++)
                            {
                                using (var cmd = new NpgsqlCommand(
                                    "UPDATE public.\"Cells\" SET \"Value\"=@val " +
                                    "WHERE \"GameId\"=@gid AND \"Row\"=@row AND \"Column\"=@col", conn, tx))
                                {
                                    cmd.Parameters.AddWithValue("val", game.Field[r, c]);
                                    cmd.Parameters.AddWithValue("gid", game.Id);
                                    cmd.Parameters.AddWithValue("row", r);
                                    cmd.Parameters.AddWithValue("col", c);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                    }
                    tx.Commit();
                }
            }
        }

        public void Remove(int gameId)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Cells\" WHERE \"GameId\"=@gid", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("gid", gameId);
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new NpgsqlCommand("DELETE FROM public.\"Games\" WHERE \"Id\"=@gid", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("gid", gameId);
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
        }
    }
}
