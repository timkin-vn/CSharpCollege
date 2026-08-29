using System;
using System.Collections.Generic;
using System.Linq;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Interfaces.Services;
using Pacman.Common.Models;

namespace Pacman.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapRepository _mapRepository;
        private static readonly Random _random = new Random();

        public GameService(IGameRepository gameRepository, IMapRepository mapRepository)
        {
            _gameRepository = gameRepository;
            _mapRepository = mapRepository;
        }

        public GameStateDto CreateNewGame(int userId)
        {
            var map = _mapRepository.GetDefaultMap();
            if (map == null)
                throw new Exception("Default map not found");

            return _gameRepository.CreateGame(userId, map.Id);
        }

        public GameStateDto GetGameState(int gameId)
        {
            return _gameRepository.GetGameState(gameId);
        }

        public GameStateDto Move(int gameId, Direction direction)
        {
            var state = _gameRepository.GetGameState(gameId);
            if (state == null || state.Game.Status != GameStatus.InProgress)
                return state;

            
            var pacman = state.Actors.First(a => a.ActorType == ActorType.Pacman);
            var newPacmanPos = GetNewPosition(pacman.Row, pacman.Col, direction);

            if (IsValidMove(state.Map, newPacmanPos.Row, newPacmanPos.Col))
            {
                pacman.Row = newPacmanPos.Row;
                pacman.Col = newPacmanPos.Col;
                pacman.Direction = direction;

                
                var collectible = state.CollectibleStates.FirstOrDefault(c =>
                    c.Row == pacman.Row && c.Col == pacman.Col && !c.IsEaten);

                if (collectible != null)
                {
                    collectible.IsEaten = true;

                    if (collectible.CollectibleType == CellType.Pellet)
                    {
                        state.Game.Score += Constants.PelletScore;
                    }
                    else if (collectible.CollectibleType == CellType.PowerPellet)
                    {
                        state.Game.Score += Constants.PowerPelletScore;

                        
                        foreach (var ghost in state.Actors.Where(a => a.ActorType != ActorType.Pacman))
                        {
                            ghost.FrightenedTicksLeft = Constants.FrightenedDuration;
                        }
                    }
                }
            }

            
            MoveGhosts(state);

            
            CheckCollisions(state);

            
            if (state.CollectibleStates.All(c => c.IsEaten))
            {
                state.Game.Status = GameStatus.Won;
            }

            _gameRepository.UpdateGameState(state);
            return state;
        }

        private void MoveGhosts(GameStateDto state)
        {
            var ghosts = state.Actors.Where(a => a.ActorType != ActorType.Pacman).ToList();

            foreach (var ghost in ghosts)
            {
                if (ghost.FrightenedTicksLeft > 0)
                    ghost.FrightenedTicksLeft--;

                var possibleDirections = GetPossibleDirections(state.Map, ghost.Row, ghost.Col, ghost.Direction);

                if (possibleDirections.Count > 0)
                {
                    var newDirection = possibleDirections[_random.Next(possibleDirections.Count)];
                    var newPos = GetNewPosition(ghost.Row, ghost.Col, newDirection);

                    ghost.Row = newPos.Row;
                    ghost.Col = newPos.Col;
                    ghost.Direction = newDirection;
                }
            }
        }

        private List<Direction> GetPossibleDirections(MapDto map, int row, int col, Direction currentDirection)
        {
            var directions = new List<Direction>();
            var opposite = GetOppositeDirection(currentDirection);

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (dir == Direction.None || dir == opposite)
                    continue;

                var newPos = GetNewPosition(row, col, dir);
                if (IsValidMove(map, newPos.Row, newPos.Col))
                {
                    directions.Add(dir);
                }
            }

            
            if (directions.Count == 0 && opposite != Direction.None)
            {
                var newPos = GetNewPosition(row, col, opposite);
                if (IsValidMove(map, newPos.Row, newPos.Col))
                {
                    directions.Add(opposite);
                }
            }

            return directions;
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                default: return Direction.None;
            }
        }

        private void CheckCollisions(GameStateDto state)
        {
            var pacman = state.Actors.First(a => a.ActorType == ActorType.Pacman);
            var ghosts = state.Actors.Where(a => a.ActorType != ActorType.Pacman).ToList();

            foreach (var ghost in ghosts)
            {
                if (ghost.Row == pacman.Row && ghost.Col == pacman.Col)
                {
                    if (ghost.FrightenedTicksLeft > 0)
                    {
                        
                        state.Game.Score += Constants.GhostScore;
                        ghost.FrightenedTicksLeft = 0;

                        
                        ghost.Row = 1;
                        ghost.Col = state.Map.ColCount / 2;
                        ghost.Direction = Direction.Down;
                    }
                    else
                    {
                        
                        state.Game.Lives--;

                        if (state.Game.Lives <= 0)
                        {
                            state.Game.Status = GameStatus.Lost;
                        }
                        else
                        {
                            
                            ResetActorPositions(state);
                        }
                        break;
                    }
                }
            }
        }

        private void ResetActorPositions(GameStateDto state)
        {
            var pacman = state.Actors.First(a => a.ActorType == ActorType.Pacman);
            pacman.Row = state.Map.RowCount - 2;
            pacman.Col = state.Map.ColCount / 2;
            pacman.Direction = Direction.None;

            var ghosts = state.Actors.Where(a => a.ActorType != ActorType.Pacman).ToList();
            int ghostCol = state.Map.ColCount / 2 - 1;

            foreach (var ghost in ghosts)
            {
                ghost.Row = 1;
                ghost.Col = ghostCol++;
                ghost.Direction = Direction.Down;
                ghost.FrightenedTicksLeft = 0;
            }
        }

        private (int Row, int Col) GetNewPosition(int row, int col, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return (row - 1, col);
                case Direction.Down: return (row + 1, col);
                case Direction.Left: return (row, col - 1);
                case Direction.Right: return (row, col + 1);
                default: return (row, col);
            }
        }

        private bool IsValidMove(MapDto map, int row, int col)
        {
            if (row < 0 || row >= map.RowCount || col < 0 || col >= map.ColCount)
                return false;

            var cell = map.Cells.FirstOrDefault(c => c.Row == row && c.Col == col);
            return cell != null && cell.CellType != CellType.Wall;
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.RemoveGame(gameId);
        }

        public IReadOnlyList<GameDto> GetUserGames(int userId)
        {
            return _gameRepository.GetGamesByUserId(userId);
        }
    }
}