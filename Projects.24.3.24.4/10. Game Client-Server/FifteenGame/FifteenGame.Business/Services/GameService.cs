using FifteenGame.Common.Contracts;
using FifteenGame.Common.Models;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        public GameModel GetOrCreateGame(int userId)
        {
            using (var context = new GameContext())
            {
                var activeGame = context.Games.OrderByDescending(g => g.Id).FirstOrDefault(g => g.UserId == userId && g.Result == 0);

                if (activeGame == null)
                {
                    var lastGame = context.Games.OrderByDescending(g => g.Id).FirstOrDefault(g => g.UserId == userId);

                    activeGame = new GameEntity
                    {
                        UserId = userId,
                        BoardState = "         ",
                        Result = 0,
                        PlayerScore = lastGame?.PlayerScore ?? 0,
                        ComputerScore = lastGame?.ComputerScore ?? 0
                    };
                    context.Games.Add(activeGame);
                    context.SaveChanges();
                }

                return new GameModel
                {
                    Id = activeGame.Id,
                    UserId = activeGame.UserId,
                    BoardState = activeGame.BoardState,
                    Result = (GameResult)activeGame.Result,
                    PlayerScore = activeGame.PlayerScore,
                    ComputerScore = activeGame.ComputerScore
                };
            }
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                if (game == null || game.Result != 0) return null;

                char[] board = game.BoardState.ToCharArray();
                int index = row * 3 + column;

                if (board[index] != ' ') return null;
                board[index] = 'X';
                game.Result = (int)CheckWin(board, 'X');

                if (game.Result == 0)
                {
                    int computerMove = FindBestMove(board);
                    if (computerMove != -1)
                    {
                        board[computerMove] = 'O';
                        game.Result = (int)CheckWin(board, 'O');
                    }
                }

                if (game.Result == (int)GameResult.PlayerWin) game.PlayerScore++;
                else if (game.Result == (int)GameResult.ComputerWin) game.ComputerScore++;

                game.BoardState = new string(board);
                context.SaveChanges();

                return new GameModel
                {
                    Id = game.Id,
                    UserId = game.UserId,
                    BoardState = game.BoardState,
                    Result = (GameResult)game.Result,
                    PlayerScore = game.PlayerScore,
                    ComputerScore = game.ComputerScore
                };
            }
        }

        public void ResetGame(int gameId)
        {
            using (var context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                if (game != null)
                {
                    game.BoardState = "         ";
                    game.Result = 0;
                    context.SaveChanges();
                }
            }
        }

        private int FindBestMove(char[] board)
        {
            int winMove = FindWinningMove(board, 'O');
            if (winMove != -1) return winMove;

            int blockMove = FindWinningMove(board, 'X');
            if (blockMove != -1) return blockMove;

            if (board[4] == ' ') return 4;

            int[] corners = { 0, 2, 6, 8 };
            foreach (var c in corners)
            {
                if (board[c] == ' ') return c;
            }

            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ') return i;
            }

            return -1;
        }

        private int FindWinningMove(char[] board, char player)
        {
            int[][] wins = new int[][]
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
                new[] {0,4,8}, new[] {2,4,6}
            };

            foreach (var w in wins)
            {
                int countPlayer = 0;
                int emptyIndex = -1;

                foreach (var i in w)
                {
                    if (board[i] == player) countPlayer++;
                    else if (board[i] == ' ') emptyIndex = i;
                }

                if (countPlayer == 2 && emptyIndex != -1)
                {
                    return emptyIndex;
                }
            }

            return -1;
        }

        private GameResult CheckWin(char[] board, char player)
        {
            int[][] wins = new int[][]
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
                new[] {0,4,8}, new[] {2,4,6}
            };

            foreach (var w in wins)
            {
                if (board[w[0]] == player && board[w[1]] == player && board[w[2]] == player)
                    return player == 'X' ? GameResult.PlayerWin : GameResult.ComputerWin;
            }

            if (!board.Contains(' ')) return GameResult.Draw;
            return GameResult.None;
        }
    }
}