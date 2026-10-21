using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.Common.Definitions;
using MemoryGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        private readonly Random _random = new Random();

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public GameModel GetByGameId(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public GameModel GetByUserId(int userId)
        {
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();
            if (dto != null)
            {
                var lastGame = FromDto(dto);
                if (!IsGameOver(lastGame))
                {
                    return lastGame;
                }
            }

            var game = new GameModel
            {
                UserId = userId,
            };

            Shuffle(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            var values = new List<int>();
            for (int value = 1; value <= Constants.PairCount; value++)
            {
                values.Add(value);
                values.Add(value);
            }

            int index = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = values[index++];
                    model.SetState(row, column, CardState.Hidden);
                }
            }

            model.MoveCount = 0;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            // Алгоритм Фишера-Йетса
            int cellCount = Constants.RowCount * Constants.ColumnCount;
            for (int i = cellCount - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);

                int rowI = i / Constants.ColumnCount, columnI = i % Constants.ColumnCount;
                int rowJ = j / Constants.ColumnCount, columnJ = j % Constants.ColumnCount;

                int temp = model[rowI, columnI];
                model[rowI, columnI] = model[rowJ, columnJ];
                model[rowJ, columnJ] = temp;
            }
        }

        public bool? IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            if (dto == null)
            {
                return null;
            }

            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            return model.MatchedPairCount == Constants.PairCount;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var dto = _repository.GetByGameId(gameId);
            var model = FromDto(dto);
            if (model == null)
            {
                return null;
            }

            if (MakeMove(model, row, column))
            {
                _repository.Save(ToDto(model));
            }

            return model;
        }

        /// <summary>
        /// Ход игрока - щелчок по карточке.
        /// Если открыты две несовпавшие карточки, они сначала закрываются.
        /// Затем, если выбранная карточка закрыта, она открывается.
        /// Если открыта вторая карточка, засчитывается попытка, а совпавшая пара остается открытой навсегда.
        /// </summary>
        public bool MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)
            {
                return false;
            }

            bool changed = false;
            var opened = GetOpenedCells(model);

            if (opened.Count == 2)
            {
                foreach (var (openedRow, openedColumn) in opened)
                {
                    model.SetState(openedRow, openedColumn, CardState.Hidden);
                }

                opened.Clear();
                changed = true;
            }

            if (model.GetState(row, column) != CardState.Hidden)
            {
                return changed;
            }

            model.SetState(row, column, CardState.Opened);
            opened.Add((row, column));

            if (opened.Count == 2)
            {
                model.MoveCount++;

                var (firstRow, firstColumn) = opened[0];
                if (model[firstRow, firstColumn] == model[row, column])
                {
                    model.SetState(firstRow, firstColumn, CardState.Matched);
                    model.SetState(row, column, CardState.Matched);
                                }
            }

            return true;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        private List<(int Row, int Column)> GetOpenedCells(GameModel model)
        {
            var result = new List<(int Row, int Column)>();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model.GetState(row, column) == CardState.Opened)
                    {
                        result.Add((row, column));
                    }
                }
            }

            return result;
        }

        private GameModel FromDto(GameDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new GameModel
                        {
                            Id = dto.Id,
                            UserId = dto.UserId,
                            MoveCount = dto.MoveCount,
                        };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = dto.Cells[row, column];
                    result.SetState(row, column, (CardState)dto.States[row, column]);
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            var result = new GameDto
                        {
                            Id = model.Id,
                            UserId = model.UserId,
                            MoveCount = model.MoveCount,
                        };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.Cells[row, column] = model[row, column];
                    result.States[row, column] = (int)model.GetState(row, column);
                }
            }

            return result;
        }
    }
}
