using Npgsql;
using TicTacToe.Common.Models;

namespace TicTacToe.DataAccess.Repositories
{
    public class GameRepository
    {
        private readonly string _connString = "Host=localhost;Username=postgres;Password=1234;Database=postgres";

        public void UpdateGame(int id, string board, bool isXTurn)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("UPDATE Games SET BoardState = @b, IsXTurn = @t WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("b", board);
                cmd.Parameters.AddWithValue("t", isXTurn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}