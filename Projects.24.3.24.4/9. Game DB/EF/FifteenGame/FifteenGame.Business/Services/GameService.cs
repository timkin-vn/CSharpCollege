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

        private const int IncomePerPerson = 7;   // Доход за сытого человека
        private const int PenaltyPerPerson = 5;  // Штраф за голодного человека

        public GameService(IGameRepository repository)
        {
            _repository = repository;
        }

        public int GetUserWinStreak(int userId)
        {
            return _repository.GetWinStreak(userId);
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
                var model = FromDto(dto);

                if (IsGameOver(model))
                {
                    // Cбрасываем
                    model.Money = Constants.InitialMoney;
                    model.TurnsPlayed = 0;
                    Initialize(model); // Генерируем новую карту шаурмичных

                    // Сохраняем обновленный сброшенный вариант в БД
                    _repository.Save(ToDto(model));
                }

                return model;
            }

            // Если игры вообще не было в базе создаем абсолютно чистую
            var game = new GameModel
            {
                UserId = userId,
                Money = Constants.InitialMoney,
                TurnsPlayed = 0
            };
            Initialize(game);
            _repository.Save(ToDto(game));

            return game;
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
                    // Генерируем случайное количество людей от 0 до 50
                    model.SetPeopleCount(row, column, _random.Next(0, 51));
                    model.SetHasShop(row, column, false);
                    model.SetIsVeggie(row, column, false);
                    model.SetIsRevealed(row, column, false);
                }
            }

            // Создаем от 1 до 3 ЗОЖ клеток случайным образом
            int veggieCount = _random.Next(1, 4);
            int placed = 0;
            while (placed < veggieCount)
            {
                int r = _random.Next(0, Constants.RowCount);
                int c = _random.Next(0, Constants.ColumnCount);
                if (!model.GetIsVeggie(r, c))
                {
                    model.SetIsVeggie(r, c, true);
                    placed++;
                }
            }
        }

        public void Move(GameModel model, int row, int column)
        {
            // Если игра окончена или там уже стоит ларёк ничего не делаем
            if (model.TurnsPlayed >= Constants.TargetTurns || model.GetHasShop(row, column)) return;

            // Списываем стоимость постройки ларька
            model.Money -= Constants.ShopCost;
            model.SetHasShop(row, column, true);

            // Раскрываем саму клетку и её соседей крестом
            model.SetIsRevealed(row, column, true);
            if (row > 0) model.SetIsRevealed(row - 1, column, true);
            if (row < Constants.RowCount - 1) model.SetIsRevealed(row + 1, column, true);
            if (column > 0) model.SetIsRevealed(row, column - 1, true);
            if (column < Constants.ColumnCount - 1) model.SetIsRevealed(row, column + 1, true);

            // Считаем экономику за этот ход по всей карте
            int totalIncome = 0;
            int totalPenalty = 0;

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    int people = model.GetPeopleCount(r, c);

                    // Если клетка покрыта хотя бы одним ларьком
                    if (IsCellCovered(model, r, c))
                    {
                        // ЗОЖники в зоне покрытия ларька прибыль не приносят
                        if (!model.GetIsVeggie(r, c))
                        {
                            totalIncome += people * IncomePerPerson;
                        }
                    }
                    else
                    {
                        // Если клетка не покрыта ларьком обычные люди штрафуют ЗОЖники нет
                        if (!model.GetIsVeggie(r, c))
                        {
                            totalPenalty += people * PenaltyPerPerson;
                        }
                    }
                }
            }

            // Применяем доходы и штрафы к балансу
            model.Money += totalIncome;
            model.Money -= totalPenalty;

            // Увеличиваем счётчик сыгранных ходов
            model.TurnsPlayed++;

            if (IsGameOver(model))
            {
                int currentStreak = _repository.GetWinStreak(model.UserId);
                if (model.Money >= 0)
                {
                    _repository.UpdateWinStreak(model.UserId, currentStreak + 1); // Победа
                }
                else
                {
                    _repository.UpdateWinStreak(model.UserId, 0); // Банкротство
                }
            }

            _repository.Save(ToDto(model));
        }

        public bool IsGameOver(int gameId)
        {
            var dto = _repository.GetByGameId(gameId);
            return IsGameOver(FromDto(dto));
        }

        public bool IsGameOver(GameModel model)
        {
            // Игра окончена если кончились ходы или если баланс ушел в минус
            return model.TurnsPlayed >= Constants.TargetTurns || model.Money < 0;
        }

        public GameModel RestartGame(int userId)
        {
            // Ищем старую игру пользователя в базе
            var dtos = _repository.GetByUserId(userId);
            var dto = dtos.LastOrDefault();

            GameModel model;

            if (dto != null)
            {
                // Если нашли берем её и полностью обнуляем
                model = FromDto(dto);
            }
            else
            {
                // Если вдруг игры не было создаем объект с нуля
                model = new GameModel { UserId = userId };
            }

            // Сбрасываем все параметры к стартовым
            model.Money = Constants.InitialMoney;
            model.TurnsPlayed = 0;
            InitializeNewMap(model); // Наш приватный метод генерации карты с зожниками

            // Сохраняем свежее состояние в БД
            _repository.Save(ToDto(model));

            return model;
        }

        public void RemoveGame(int gameId)
        {
            _repository.Remove(gameId);
        }

        // Проверка радиуса действия крестом
        public bool IsCellCovered(GameModel model, int row, int column)
        {
            if (model.GetHasShop(row, column)) return true;
            if (row > 0 && model.GetHasShop(row - 1, column)) return true;
            if (row < Constants.RowCount - 1 && model.GetHasShop(row + 1, column)) return true;
            if (column > 0 && model.GetHasShop(row, column - 1)) return true;
            if (column < Constants.ColumnCount - 1 && model.GetHasShop(row, column + 1)) return true;

            return false;
        }

        // Считаем зожников вокруг клетки
        public int GetVeggieNeighborsCount(GameModel model, int r, int c)
        {
            int count = 0;
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    int nr = r + dr;
                    int nc = c + dc;
                    if (nr >= 0 && nr < Constants.RowCount && nc >= 0 && nc < Constants.ColumnCount)
                    {
                        if (model.GetIsVeggie(nr, nc)) count++;
                    }
                }
            }
            return count;
        }

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