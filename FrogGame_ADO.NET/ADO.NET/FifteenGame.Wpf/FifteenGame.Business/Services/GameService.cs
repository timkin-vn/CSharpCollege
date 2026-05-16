using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    model[row, column] = (int)CellType.Water;
                }
            }

            model.FrogRow = 0;
            model.FrogColumn = 0;
            model[model.FrogRow, model.FrogColumn] = (int)CellType.Frog;

            model.HomeRow = 7;
            model.HomeColumn = 7;
            model[model.HomeRow, model.HomeColumn] = (int)CellType.Home;

            CreateLilyPadPath(model);

            AddRandomAlgae(model, 8);

            model.MoveCount = 0;
            model.IsGameOver = false;
            model.IsWin = false;
            model.SelectedLilyPadRow = null;
            model.SelectedLilyPadColumn = null;
        }

        private void CreateLilyPadPath(GameModel model)
        {
            var path = FindPath(model.FrogRow, model.FrogColumn, model.HomeRow, model.HomeColumn);

            foreach (var (row, column) in path)
            {
                if (model[row, column] != (int)CellType.Frog && model[row, column] != (int)CellType.Home)
                {
                    model[row, column] = (int)CellType.LilyPad;
                }
            }

            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int row = random.Next(8);
                int column = random.Next(8);
                if (model[row, column] == (int)CellType.Water)
                {
                    model[row, column] = (int)CellType.LilyPad;
                }
            }
        }

        private List<(int row, int column)> FindPath(int startRow, int startCol, int endRow, int endCol)
        {
            var path = new List<(int, int)>();
            int currentRow = startRow;
            int currentCol = startCol;

            while (currentRow != endRow || currentCol != endCol)
            {
                if (currentRow < endRow) currentRow++;
                else if (currentRow > endRow) currentRow--;

                if (currentCol < endCol) currentCol++;
                else if (currentCol > endCol) currentCol--;

                if (currentRow != endRow || currentCol != endCol)
                {
                    path.Add((currentRow, currentCol));
                }
            }

            return path;
        }

        private void AddRandomAlgae(GameModel model, int count)
        {
            int placed = 0;
            var random = new Random();
            while (placed < count)
            {
                int row = random.Next(8);
                int column = random.Next(8);

                if (model[row, column] == (int)CellType.LilyPad)
                {
                    model[row, column] = (int)CellType.Algae;
                    placed++;
                }
            }
        }

        public bool RemoveAlgae(GameModel model, int row, int column)
        {
            if (model.IsGameOver || (CellType)model[row, column] != CellType.Algae)
                return false;

            model[row, column] = (int)CellType.LilyPad;
            model.MoveCount++;
            model.SelectedLilyPadRow = null;
            model.SelectedLilyPadColumn = null;

            CheckAndExecuteWin(model);

            if (!model.IsGameOver)
            {
                MoveFrogOneStep(model);
                CheckAndExecuteWin(model);
            }

            return true;
        }

        public bool MoveLilyPad(GameModel model, int targetRow, int targetColumn)
        {
            if (model.IsGameOver ||
                !model.SelectedLilyPadRow.HasValue ||
                !model.SelectedLilyPadColumn.HasValue ||
                (CellType)model[targetRow, targetColumn] != CellType.Water)
                return false;

            int sourceRow = model.SelectedLilyPadRow.Value;
            int sourceColumn = model.SelectedLilyPadColumn.Value;

            model[targetRow, targetColumn] = (int)CellType.LilyPad;
            model[sourceRow, sourceColumn] = (int)CellType.Water;
            model.MoveCount++;

            model.SelectedLilyPadRow = null;
            model.SelectedLilyPadColumn = null;

            CheckAndExecuteWin(model);

            if (!model.IsGameOver)
            {
                MoveFrogOneStep(model);
                CheckAndExecuteWin(model);
            }

            return true;
        }

        public bool SelectLilyPad(GameModel model, int row, int column)
        {
            if (model.IsGameOver || model[row, column] != (int)CellType.LilyPad)
                return false;

            if (model.SelectedLilyPadRow == row && model.SelectedLilyPadColumn == column)
            {
                model.SelectedLilyPadRow = null;
                model.SelectedLilyPadColumn = null;
                return true;
            }

            model.SelectedLilyPadRow = row;
            model.SelectedLilyPadColumn = column;
            return true;
        }
        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            throw new NotImplementedException("Будет реализовано после адаптации репозитория");
        }

        public bool IsGameOver(int gameId)
        {
            throw new NotImplementedException("Будет реализовано после адаптации репозитория");
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                return FromDto(dto);
            }

            var game = new GameModel { UserId = userId };
            Initialize(game);
            var gameId = _repository.Save(ToDto(game));
            return GetByGameId(gameId);
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private GameModel FromDto(GameDto dto)
        {
            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                FrogRow = dto.FrogRow,
                FrogColumn = dto.FrogColumn,
                HomeRow = dto.HomeRow,
                HomeColumn = dto.HomeColumn,
                IsGameOver = dto.IsGameOver,
                IsWin = dto.IsWin,
                SelectedLilyPadRow = dto.SelectedLilyPadRow,
                SelectedLilyPadColumn = dto.SelectedLilyPadColumn,
            };

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel game)
        {
            var dto = new GameDto
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                FrogRow = game.FrogRow,
                FrogColumn = game.FrogColumn,
                HomeRow = game.HomeRow,
                HomeColumn = game.HomeColumn,
                IsGameOver = game.IsGameOver,
                IsWin = game.IsWin,
                SelectedLilyPadRow = game.SelectedLilyPadRow,
                SelectedLilyPadColumn = game.SelectedLilyPadColumn,
            };

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    dto.Cells[row, column] = game[row, column];
                }
            }

            return dto;
        }
        public void SaveGame(GameModel game)
        {
            var dto = ToDto(game);
            var gameId = _repository.Save(dto);
            if (game.Id == 0)
            {
                game.Id = gameId;
            }
        }
        private void CheckAndExecuteWin(GameModel model)
        {
            if (model.IsGameOver) return;

            if (HasClearPathToHome(model) || IsFrogNextToHome(model))
            {
                model.IsGameOver = true;
                model.IsWin = true;

                if (model.BestMoveCount == 0 || model.MoveCount < model.BestMoveCount)
                {
                    model.BestMoveCount = model.MoveCount;
                }
            }
        }

        private bool HasClearPathToHome(GameModel model)
        {
            var visited = new bool[8, 8];
            return CheckClearPath(model.FrogRow, model.FrogColumn, visited, model);
        }

        private bool CheckClearPath(int row, int col, bool[,] visited, GameModel model)
        {
            if (row < 0 || row >= 8 || col < 0 || col >= 8)
                return false;

            if (visited[row, col])
                return false;

            visited[row, col] = true;

            if (row == model.HomeRow && col == model.HomeColumn)
                return true;

            var cellType = (CellType)model[row, col];
            if (cellType != CellType.LilyPad && cellType != CellType.Frog && cellType != CellType.Home)
                return false;

            return CheckClearPath(row + 1, col, visited, model) ||
                   CheckClearPath(row - 1, col, visited, model) ||
                   CheckClearPath(row, col + 1, visited, model) ||
                   CheckClearPath(row, col - 1, visited, model);
        }

        private void MoveFrogAlongPathToHome(GameModel model)
        {
            var path = FindShortestPathToHome(model);

            if (path != null && path.Count > 0)
            {
                foreach (var (row, col) in path)
                {
                    MoveFrogTo(model, row, col);
                }
            }
        }

        private List<(int row, int column)> FindShortestPathToHome(GameModel model)
        {
            var visited = new bool[8, 8];
            var queue = new Queue<List<(int, int)>>();

            queue.Enqueue(new List<(int, int)> { (model.FrogRow, model.FrogColumn) });
            visited[model.FrogRow, model.FrogColumn] = true;

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var (row, col) = path.Last();
                if (row == model.HomeRow && col == model.HomeColumn)
                {
                    return path.Skip(1).ToList();
                }

                foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int newRow = row + dr;
                    int newCol = col + dc;

                    if (newRow >= 0 && newRow < 8 &&
                        newCol >= 0 && newCol < 8 &&
                        !visited[newRow, newCol] &&
                        ((CellType)model[newRow, newCol] == CellType.LilyPad || (CellType)model[newRow, newCol] == CellType.Home))
                    {
                        visited[newRow, newCol] = true;
                        var newPath = new List<(int, int)>(path) { (newRow, newCol) };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return null;
        }

        private bool IsFrogNextToHome(GameModel model)
        {
            int frogRow = model.FrogRow;
            int frogCol = model.FrogColumn;
            int homeRow = model.HomeRow;
            int homeCol = model.HomeColumn;

            return (frogRow == homeRow && frogCol == homeCol - 1) ||
                   (frogRow == homeRow && frogCol == homeCol + 1) ||
                   (frogRow == homeRow - 1 && frogCol == homeCol) ||
                   (frogRow == homeRow + 1 && frogCol == homeCol);
        }

        private void MoveFrogToHome(GameModel model)
        {
            model[model.FrogRow, model.FrogColumn] = (int)CellType.LilyPad;
            model.FrogRow = model.HomeRow;
            model.FrogColumn = model.HomeColumn;
            model[model.HomeRow, model.HomeColumn] = (int)CellType.Frog;
        }

        private void MoveFrogTo(GameModel model, int newRow, int newCol)
        {
            model[model.FrogRow, model.FrogColumn] = (int)CellType.LilyPad;
            model.FrogRow = newRow;
            model.FrogColumn = newCol;
            model[newRow, newCol] = (int)CellType.Frog;
        }

        public void MoveFrogOneStep(GameModel model)
        {
            if (model.IsGameOver) return;

            int currentRow = model.FrogRow;
            int currentCol = model.FrogColumn;

            if (currentRow < model.HomeRow && (CellType)model[currentRow + 1, currentCol] == CellType.LilyPad)
            {
                MoveFrogTo(model, currentRow + 1, currentCol);
            }
            else if (currentCol < model.HomeColumn && (CellType)model[currentRow, currentCol + 1] == CellType.LilyPad)
            {
                MoveFrogTo(model, currentRow, currentCol + 1);
            }
            else if (currentRow > model.HomeRow && (CellType)model[currentRow - 1, currentCol] == CellType.LilyPad)
            {
                MoveFrogTo(model, currentRow - 1, currentCol);
            }
            else if (currentCol > model.HomeColumn && (CellType)model[currentRow, currentCol - 1] == CellType.LilyPad)
            {
                MoveFrogTo(model, currentRow, currentCol - 1);
            }
        }
    }


}