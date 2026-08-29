using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly UserService _userService; 
        private readonly Random _random = new Random();

        public GameService(IGameRepository gameRepository, UserService userService)
        {
            _gameRepository = gameRepository;
            _userService = userService;
        }

        public StartGameReply StartNewGame(int userId)
        {
            GameModel model = null;

            
            var savedGame = _userService.LoadSavedGame(userId);

            if (savedGame != null && !savedGame.IsGameOver)
            {
                
                model = savedGame;

                
            }
            else
            {
                
                _userService.ClearSavedGameAfterFinish(userId);

                model = new GameModel
                {
                    IsPlayerTurn = true,
                    IsGameOver = false,
                    PlayerCells = InitializeEmptyField(),
                    EnemyCells = InitializeEmptyField()
                };

                PlaceShipsRandomly(model.PlayerCells);
                PlaceShipsRandomly(model.EnemyCells);

               
                _userService.SaveCurrentGame(userId, model);
            }

            
            int gameId = _gameRepository.SaveGame(model, userId);
            model.Id = gameId; 
            var visibleEnemyField = model.EnemyCells.Select(c => new CellDto
            {
                X = c.X,
                Y = c.Y,
                State = (c.State == CellState.Ship) ? CellState.Empty : c.State
            }).ToList();
            return new StartGameReply
            {
                GameId = gameId,
                PlayerField = model.PlayerCells,
                EnemyField = visibleEnemyField
            };
        }

        public GameReply MakeMove(MakeMoveRequest request)
        {
            int userId = request.UserId;
            var model = _gameRepository.LoadGame(request.GameId);

            if (model == null || model.IsGameOver)
            {
                
                return new GameReply { IsGameOver = true };
            }

            var reply = new GameReply();

            
            var targetCell = model.EnemyCells.First(c => c.X == request.X && c.Y == request.Y);

            if (targetCell.State == CellState.Ship)
            {
                targetCell.State = CellState.Hit;
                reply.ChangedEnemyCells.Add(new CellDto { X = targetCell.X, Y = targetCell.Y, State = CellState.Hit });

               
                if (CheckWin(model, reply))
                {
                   
                    SaveAndClear(model, userId);
                    return reply;
                }
            }
            else if (targetCell.State == CellState.Empty)
            {
                targetCell.State = CellState.Miss;
                reply.ChangedEnemyCells.Add(new CellDto { X = targetCell.X, Y = targetCell.Y, State = CellState.Miss });

                
                model.IsPlayerTurn = false;
                AiTurn(model, reply);

                
                CheckWin(model, reply);
            }
            else
            {
                
                reply.ChangedEnemyCells.Add(new CellDto { X = targetCell.X, Y = targetCell.Y, State = targetCell.State });
            }

           
            SaveAndClear(model, userId);

            return reply;
        }

        private void SaveAndClear(GameModel model, int userId)
        {
            _gameRepository.SaveGame(model, userId);
            if (!model.IsGameOver)
                _userService.SaveCurrentGame(userId, model);
            else
                _userService.ClearSavedGameAfterFinish(userId);
        }

        
        private List<CellDto> InitializeEmptyField()
        {
            var list = new List<CellDto>();
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                    list.Add(new CellDto { X = x, Y = y, State = CellState.Empty });
            return list;
        }

        private void PlaceShipsRandomly(List<CellDto> field)
        {
            var shipSizes = new[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            var random = new Random();

            foreach (var size in shipSizes)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = random.Next(10);
                    int y = random.Next(10);
                    bool horizontal = random.Next(2) == 0;

                    if (CanPlaceShip(field, x, y, size, horizontal))
                    {
                        PlaceShip(field, x, y, size, horizontal);
                        placed = true;
                    }
                }
            }
        }

        private bool CanPlaceShip(List<CellDto> field, int startX, int startY, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int x = startX + (horizontal ? i : 0);
                int y = startY + (horizontal ? 0 : i);

                if (x >= 10 || y >= 10)
                    return false;

                var cell = field.First(c => c.X == x && c.Y == y);
                if (cell.State == CellState.Ship)
                    return false;

               
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;
                        if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10)
                        {
                            if (field.First(c => c.X == nx && c.Y == ny).State == CellState.Ship)
                                return false;
                        }
                    }
            }
            return true;
        }

        private void PlaceShip(List<CellDto> field, int startX, int startY, int size, bool horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                int x = startX + (horizontal ? i : 0);
                int y = startY + (horizontal ? 0 : i);
                field.First(c => c.X == x && c.Y == y).State = CellState.Ship;
            }
        }

        private void AiTurn(GameModel model, GameReply reply)
        {
            bool botTurn = true;
            while (botTurn)
            {
                var available = model.PlayerCells
                    .Where(c => c.State == CellState.Empty || c.State == CellState.Ship)
                    .ToList();

                if (available.Count == 0) break;

                var target = available[_random.Next(available.Count)];

                if (target.State == CellState.Ship)
                {
                    target.State = CellState.Hit;
                    reply.ChangedPlayerCells.Add(new CellDto { X = target.X, Y = target.Y, State = CellState.Hit });
                }
                else
                {
                    target.State = CellState.Miss;
                    reply.ChangedPlayerCells.Add(new CellDto { X = target.X, Y = target.Y, State = CellState.Miss });
                    botTurn = false;
                    model.IsPlayerTurn = true;
                }

                if (CheckWin(model, reply)) return;
            }
        }

        private bool CheckWin(GameModel model, GameReply reply)
        {
            if (!model.EnemyCells.Any(c => c.State == CellState.Ship))
            {
                model.IsGameOver = true;
                reply.IsGameOver = true;
                reply.Winner = "Player";
                return true;
            }

            if (!model.PlayerCells.Any(c => c.State == CellState.Ship))
            {
                model.IsGameOver = true;
                reply.IsGameOver = true;
                reply.Winner = "Opponent";
                return true;
            }

            return false;
        }
    }
}