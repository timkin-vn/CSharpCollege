using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;
        private static readonly Random _random = new Random();

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
                return FromDto(dto);
            }

            var game = new GameModel
            {
                UserId = userId,
                Money = Constants.InitialMoney,
                TurnsPlayed = 0
            };

            InitializeNewMap(game);
            var gameId = _repository.Save(ToDto(game));

            return GetByGameId(gameId);
        }

        public void Initialize(GameModel model)
        {
            model.Money = Constants.InitialMoney;
            model.TurnsPlayed = 0;
            InitializeNewMap(model);
        }

        private void InitializeNewMap(GameModel model)
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model.SetPeopleCount(row, column, _random.Next(10, 51));
                    model.SetHasShop(row, column, false);
                    model.SetIsVeggie(row, column, _random.Next(0, 4) == 0);
                    model.SetIsRevealed(row, column, false);
                }
            }

            int centerRow = Constants.RowCount / 2;
            int centerCol = Constants.ColumnCount / 2;
            model.SetIsRevealed(centerRow, centerCol, true);
        }

        // Метод для кликов по координатам
        public void Move(GameModel model, int row, int column)
        {
            if (model.TurnsPlayed >= Constants.TargetTurns) return;

            if (!model.GetIsRevealed(row, column))
            {
                if (model.Money >= 50)
                {
                    model.Money -= 50;
                    model.SetIsRevealed(row, column, true);
                    model.TurnsPlayed++;
                    _repository.Save(ToDto(model));
                }
                return;
            }

            if (model.GetIsRevealed(row, column) && !model.GetHasShop(row, column))
            {
                if (model.Money >= Constants.ShopCost)
                {
                    model.Money -= Constants.ShopCost;
                    model.SetHasShop(row, column, true);

                    int profit = model.GetPeopleCount(row, column) * (model.GetIsVeggie(row, column) ? 15 : 10);
                    model.Money += profit;

                    model.TurnsPlayed++;
                    _repository.Save(ToDto(model));
                }
            }
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            return model.TurnsPlayed >= Constants.TargetTurns;
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            var dto = _repository.GetByGameId(gameId);
            return FromDto(dto);
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            return false;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
        }

        //МАППИНГ
        private GameModel FromDto(GameDto dto)
        {
            if (dto == null) return null;

            var result = new GameModel
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Money = dto.Money,
                TurnsPlayed = dto.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.SetPeopleCount(row, column, dto.PeopleCount[row, column]);
                    result.SetHasShop(row, column, dto.HasShop[row, column]);
                    result.SetIsVeggie(row, column, dto.IsVeggie[row, column]);
                    result.SetIsRevealed(row, column, dto.IsRevealed[row, column]);
                }
            }

            return result;
        }

        private GameDto ToDto(GameModel model)
        {
            if (model == null) return null;

            var result = new GameDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Money = model.Money,
                MoveCount = model.TurnsPlayed
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.PeopleCount[row, column] = model.GetPeopleCount(row, column);
                    result.HasShop[row, column] = model.GetHasShop(row, column);
                    result.IsVeggie[row, column] = model.GetIsVeggie(row, column);
                    result.IsRevealed[row, column] = model.GetIsRevealed(row, column);
                }
            }

            return result;
        }
    }
}