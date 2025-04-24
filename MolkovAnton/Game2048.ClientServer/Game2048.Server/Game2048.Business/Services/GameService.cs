using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.BusinessModels;
using Game2048.Common.Definitions;
using Game2048.Common.Dtos;
using Game2048.Common.Repositories;
using Game2048.Common.Services;

namespace Game2048.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly Random _random = new Random();
    
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _gameRepository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _gameRepository.GetByUserId(userId);
            if (dtos.Any())
            {
                return FromDto(dtos.Last());
            }

            var newGame = new GameModel
            {
                UserId = userId,
                GameStart = DateTime.Now,
            };

            Initialize(newGame);
            newGame.MoveCount = 0;
            var id = _gameRepository.Save(ToDto(newGame));
            return GetByGameId(id);
        }
        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var model = new GameModel
            {
                GameId = dto.Id,
                UserId = dto.UserId,
                MoveCount = dto.MoveCount,
                GameStart = dto.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = dto.Cells[row, column];
                }
            }

            return model;
        }

        private GameDto ToDto(GameModel model)
        {
            var dto = new GameDto
            {
                Id = model.GameId,
                UserId = model.UserId,
                MoveCount = model.MoveCount,
                GameStart = model.GameStart,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    dto.Cells[row, column] = model[row, column];
                }
            }

            return dto;
        }
        public void Initialize(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = 0;
                }
            }

            AddRandomTile(model);
            AddRandomTile(model);
        }
        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == 0) 
                    {
                        return false;
                    }
                }
            }

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    int currentCell = model[row, column];

                    if (column < Constants.ColumnCount - 1 && currentCell == model[row, column + 1]) 
                    {
                        return false;
                    }

                    if (row < Constants.RowCount - 1 && currentCell == model[row + 1, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public bool IsGameOver(int gameId)
        {
            var game = FromDto(_gameRepository.GetByGameId(gameId));
            return IsGameOver(game);
        }
        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            int[,] oldCells = new int[Constants.RowCount, Constants.ColumnCount];
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    oldCells[row, column] = model[row, column];
                }
            }

            switch (direction)
            {
                case MoveDirection.Left:
                    MoveLeft(model);
                    break;
                case MoveDirection.Right:
                    MoveRight(model);
                    break;
                case MoveDirection.Up:
                    MoveUp(model);
                    break;
                case MoveDirection.Down:
                    MoveDown(model);
                    break;
            }

            bool has2048 = false;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == 2048)
                    {
                        has2048 = true;
                        break;
                    }
                }
                if (has2048) break;
            }

            if (!oldCells.Cast<int>().SequenceEqual(model.Cells.Cast<int>()))
            {
                AddRandomTile(model);
                return true;
            }

            return false;
        }

        private void MoveLeft(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                int[] line = new int[Constants.ColumnCount];
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    line[column] = model[row, column];
                }

                line = MergeLine(line, model);
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = line[column];
                }
            }
        }

        private void MoveRight(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                int[] line = new int[Constants.ColumnCount];
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    line[column] = model[row, Constants.ColumnCount - 1 - column];
                }

                line = MergeLine(line, model);
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, Constants.ColumnCount - 1 - column] = line[column];
                }
            }
        }

        private void MoveUp(GameModel model)
        {
            for (int column = 0; column < Constants.ColumnCount; column++)
            {
                int[] line = new int[Constants.RowCount];
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    line[row] = model[row, column];
                }

                line = MergeLine(line, model);
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    model[row, column] = line[row];
                }
            }
        }

        private void MoveDown(GameModel model)
        {
            for (int column = 0; column < Constants.ColumnCount; column++)
            {
                int[] line = new int[Constants.RowCount];
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    line[row] = model[Constants.RowCount - 1 - row, column];
                }

                line = MergeLine(line, model);
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    model[Constants.RowCount - 1 - row, column] = line[row];
                }
            }
        }
        private int[] MergeLine(int[] line, GameModel model)
        {
            int[] newLine = line.Where(x => x != 0).ToArray();
            for (int i = 0; i < newLine.Length - 1; i++)
            {
                if (newLine[i] == newLine[i + 1])
                {
                    newLine[i] *= 2;
                    newLine[i + 1] = 0;
                }
            }

            newLine = newLine.Where(x => x != 0).ToArray();
            return newLine.Concat(Enumerable.Repeat(0, Constants.ColumnCount - newLine.Length)).ToArray();
        }
        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var game = FromDto(_gameRepository.GetByGameId(gameId));
            MakeMove(game, direction);
            _gameRepository.Save(ToDto(game));

            return game;
        }

        public void RemoveGame(int gameId)
        {
            _gameRepository.Remove(gameId);
        }
        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new List<(int, int)>();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == 0)
                    {
                        emptyCells.Add((row, column));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var (row, column) = emptyCells[_random.Next(emptyCells.Count)];
                model[row, column] = _random.Next(10) < 9 ? 2 : 4;
            }
        }
    }
}
