using FifteenGame.BusinessProxy.Infastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FifteenGame.Core.BusinessLogic.Models;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService, IMazeGameService
    {
        public GameModel GetByGameId(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-game-id/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        #region Private Methods

        private GameModel FromDto(GameReply gameReply)
        {
            var gameModel = new GameModel
            {
                Id = gameReply.Id,
                MovesCount = gameReply.MovesCount,
                GameTime = gameReply.GameTime,
                IsOver = gameReply.IsOver,
                UserId = gameReply.UserId,
                Cells = gameReply.Cells.Select(c => new Cell { Value = c.Value, Index = c.Index }).ToList()
            };

            return gameModel;
        }

        private Level FromMazeGameReply(MazeGameReply mazeGameReply)
        {
            var level = new Level(mazeGameReply.Height, mazeGameReply.Width);
            level.PlayerRow = mazeGameReply.PlayerRow;
            level.PlayerCol = mazeGameReply.PlayerCol;
            level.ExitRow = mazeGameReply.ExitRow;
            level.ExitCol = mazeGameReply.ExitCol;
            level.Walls = new HashSet<Wall>(mazeGameReply.Walls.Select(w => new Wall(w.StartRow, w.StartCol, w.EndRow, w.EndCol)));

            return level;
        }

        #endregion

        public GameModel NewGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new NewGameRequest { UserId = userId });
                var response = httpClient.PostAsync("api/games/new", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public GameModel GetByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public bool? IsGameOver(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/is-over/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result);
                return reply.IsTrue;
            }
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new MakeMoveRequest { GameId = gameId, Direction = direction.ToString() });
                var response = httpClient.PostAsync("api/games/make-move", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public void RemoveGame(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.DeleteAsync($"api/games/remove/{gameId}").Result;
                response.EnsureSuccessStatusCode();
            }
        }

        public Level GetMazeGameByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/maze/state/{userId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<MazeGameReply>(response.Content.ReadAsStringAsync().Result);
                return FromMazeGameReply(reply);
            }
        }

        public Level MakeMazeMove(int userId, int deltaRow, int deltaCol)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new MakeMazeMoveRequest { UserId = userId, DeltaRow = deltaRow, DeltaCol = deltaCol });
                var response = httpClient.PostAsync("api/games/maze/move", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<MazeGameReply>(response.Content.ReadAsStringAsync().Result);
                return FromMazeGameReply(reply);
            }
        }

        public void StartNewMazeGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.PostAsync($"api/games/maze/start/{userId}", null).Result;
                response.EnsureSuccessStatusCode();
            }
        }

        public bool IsMazeGameOver(int userId)
        {
            // This method is not directly exposed by the Web API,
            // but the client can infer it from the game state.
            // For now, we'll return false, as the client will handle game over conditions.
            return false;
        }

        private Level FromMazeGameReply(MazeGameReply reply)
        {
            if (reply == null) return null;

            var level = new Level();
            level.ExitPosition = reply.Level.ExitPosition;
            level.DoorStates = reply.Level.DoorStates;
            level.SwitchStates = reply.Level.SwitchStates;

            level.Player = new Player(reply.Player.Row, reply.Player.Column)
            {
                Keys = reply.Player.Keys,
                Moves = reply.Player.Moves
            };

            for (int r = 0; r < Level.Rows; r++)
            {
                for (int c = 0; c < Level.Columns; c++)
                {
                    var cellDto = reply.Level.Grid[r, c];
                    if (cellDto != null)
                    {
                        level.SetCell(r, c, cellDto.Type);
                    }
                }
            }
            return level;
        }

        private GameModel FromDto(GameReply game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                FreeCellColumn = game.FreeCellColumn,
                FreeCellRow = game.FreeCellRow,
            };

            int i = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = game.Cells[i++];
                }
            }

            return result;
        }
    }
}
