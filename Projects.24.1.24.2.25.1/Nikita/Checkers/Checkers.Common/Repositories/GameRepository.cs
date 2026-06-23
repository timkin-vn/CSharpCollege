using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;
using Checkers.Common.Contracts;
using Npgsql;

namespace Checkers.Common.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Game LoadGame(int gameId)
        {
            Game loadedGame = null;
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string loadGameSql = "SELECT * FROM games WHERE id = @gameId";
                        using(var gameCommand = new NpgsqlCommand(loadGameSql, conn, transaction))
                        {
                            gameCommand.Parameters.AddWithValue("gameId", gameId);
                            using(var reader = gameCommand.ExecuteReader())
                            {
                                if(reader.Read())
                                {
                                    loadedGame = new Game();
                                    loadedGame.Id = reader.GetInt32(reader.GetOrdinal("id"));
                                    loadedGame.CurrentPlayer = (CheckerColor)Enum.Parse(typeof(CheckerColor), reader.GetString(reader.GetOrdinal("current_player")));
                                    loadedGame.Winner = (CheckerColor)Enum.Parse(typeof(CheckerColor), reader.GetString(reader.GetOrdinal("winner")));
                                    loadedGame.IsGameOver = reader.GetBoolean(reader.GetOrdinal("is_game_over"));

                                    // Инициализируем новую доску для загружаемой игры.
                                    loadedGame.Board = new Board();
                                }
                            }
                        }
                        if(loadedGame != null)
                        {
                            string loadCellsSql = "SELECT row, col, color, is_queen FROM cells WHERE game_id = @gameId";
                            using (var cellsCommand = new NpgsqlCommand(loadCellsSql, conn, transaction))
                            {
                                cellsCommand.Parameters.AddWithValue("gameId", gameId);
                                using (var reader = cellsCommand.ExecuteReader())
                                {
                                    while(reader.Read())
                                    {
                                        int row = reader.GetInt32(reader.GetOrdinal("row"));
                                        int col = reader.GetOrdinal("col");
                                        CheckerColor color = (CheckerColor)Enum.Parse(typeof(CheckerColor), reader.GetString(reader.GetOrdinal("color")));
                                        bool queen = reader.GetBoolean(reader.GetOrdinal("is_queen"));
                                        Checker checker = new Checker(color, row, col, queen);
                                        loadedGame.Board.SetChecker(row, col, checker);
                                    }
                                    
                                }
                            }
                        }
                        transaction.Commit();
                        return loadedGame;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void SaveGame(Game game)
        {   
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string gameSql = @"
                INSERT INTO games (id, white_player_id, black_player_id, current_player, winner, is_game_over)
                VALUES (@id, @whitePlayerId, @blackPlayerId, @currentPlayer, @winner, @isGameOver)
                ON CONFLICT (id) DO UPDATE SET
                    white_player_id = EXCLUDED.white_player_id,
                    black_player_id = EXCLUDED.black_player_id,
                    current_player = EXCLUDED.current_player,
                    winner = EXCLUDED.winner,
                    is_game_over = EXCLUDED.is_game_over;";

                        using (var gameCmd = new NpgsqlCommand(gameSql, conn, transaction)) {
                            gameCmd.Parameters.AddWithValue("id", game.Id); // Предполагается, что у Game есть свойство Id
                            gameCmd.Parameters.AddWithValue("whitePlayerId", game.Board.GetAllCheckers().Any(c => c.Color == CheckerColor.White) ? 1 : 0);
                            gameCmd.Parameters.AddWithValue("blackPlayerId", game.Board.GetAllCheckers().Any(c => c.Color == CheckerColor.Black) ? 1 : 0);
                            gameCmd.Parameters.AddWithValue("currentPlayer", game.CurrentPlayer.ToString());
                            gameCmd.Parameters.AddWithValue("winner", game.Winner.ToString());
                            gameCmd.Parameters.AddWithValue("isGameOver", game.IsGameOver);
                            gameCmd.ExecuteNonQuery();
                        }
                        string deleteCellsSql = "DELETE FROM cells WHERE game_id = @gameId";
                        using(var deleteCommand = new NpgsqlCommand(deleteCellsSql,conn, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("gameId", game.Id);
                            deleteCommand.ExecuteNonQuery();
                        }
                        string insertCellSql = @"
                INSERT INTO cells (game_id, row, col, color, is_queen)
                VALUES (@gameId, @row, @col, @color, @isQueen)";
                        foreach (var checker in game.Board.GetAllCheckers())
                        {
                            using (var cellCmd = new NpgsqlCommand(insertCellSql, conn, transaction)) {
                                cellCmd.Parameters.AddWithValue("gameId", game.Id);
                                cellCmd.Parameters.AddWithValue("row", checker.Row);
                                cellCmd.Parameters.AddWithValue("col", checker.Col);
                                cellCmd.Parameters.AddWithValue("color", checker.Color.ToString());
                                cellCmd.Parameters.AddWithValue("isQueen", checker.IsQueen);
                                cellCmd.ExecuteNonQuery();
                            }
                        }

                        // Если все запросы выполнились успешно, подтверждаем транзакцию.
                        transaction.Commit();
                    }catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
