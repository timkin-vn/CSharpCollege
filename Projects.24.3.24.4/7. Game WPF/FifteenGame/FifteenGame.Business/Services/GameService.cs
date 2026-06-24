using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _rnd = new Random();
        private const int IncomePerPerson = 7;  // Доход за сытого человека
        private const int PenaltyPerPerson = 5;  // Штраф за голодного человека

        public void Initialize(GameModel model)
        {
            model.Money = GameModel.InitialMoney;
            model.TurnsPlayed = 0;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model.SetPeopleCount(row, column, _rnd.Next(0, 51)); // От 0 до 50 человек
                    model.SetHasShop(row, column, false);
                    model.SetIsVeggie(row, column, false);
                    model.SetIsRevealed(row, column, false);
                }
            }

            //Создаем от 1 до 3 ЗОЖ
            int veggieCount = _rnd.Next(1, 4);
            int placed = 0;
            while (placed < veggieCount)
            {
                int r = _rnd.Next(0, GameModel.RowCount);
                int c = _rnd.Next(0, GameModel.ColumnCount);
                if (!model.GetIsVeggie(r, c))
                {
                    model.SetIsVeggie(r, c, true);
                    placed++;
                }
            }
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            // Если там уже есть шаурмичная строить нельзя ход не тратится
            if (model.GetHasShop(row, column))
            {
                return false;
            }

            // Списываем деньги за постройку
            model.Money -= GameModel.ShopCost;
            model.SetHasShop(row, column, true);
            model.TurnsPlayed++;

            // Раскрываем клетки в радиусе всех построенных ларьков
            UpdateRevealedStatus(model);

            // Считаем экономику за этот ход
            CalculateEconomy(model);

            return true;
        }
        private void UpdateRevealedStatus(GameModel model)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (IsCellCovered(model, r, c))
                    {
                        model.SetIsRevealed(r, c, true);
                    }
                }
            }
        }

        private void CalculateEconomy(GameModel model)
        {
            int turnIncome = 0;
            int turnPenalty = 0;

            // Считаем штрафы за голод только для обычных непокрытых клеток
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (!model.GetIsVeggie(r, c) && !IsCellCovered(model, r, c))
                    {
                        turnPenalty += model.GetPeopleCount(r, c) * PenaltyPerPerson;
                    }
                }
            }

            // Считаем доход от каждого построенного ларька
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (model.GetHasShop(r, c))
                    {
                        // Если сам ларёк стоит на ЗОЖ он принесет 0 дохода вообще
                        if (model.GetIsVeggie(r, c))
                        {
                            continue;
                        }

                        // Иначе собираем доход с его креста кроме ЗОЖ клеток в радиусе
                        turnIncome += GetShopRevenue(model, r, c);
                    }
                }
            }

            model.Money += turnIncome;
            model.Money -= turnPenalty;
        }

        // Считает доход конкретного ларька по кресту, игнорируя ЗОЖников в радиусе
        private int GetShopRevenue(GameModel model, int shopRow, int shopCol)
        {
            int revenue = 0;
            int[] dr = { 0, -1, 1, 0, 0 };
            int[] dc = { 0, 0, 0, -1, 1 };

            for (int i = 0; i < 5; i++)
            {
                int nr = shopRow + dr[i];
                int nc = shopCol + dc[i];

                if (nr >= 0 && nr < GameModel.RowCount && nc >= 0 && nc < GameModel.ColumnCount)
                {
                    // Если клетка в радиусе не ЗОЖ получаем с неё деньги
                    if (!model.GetIsVeggie(nr, nc))
                    {
                        revenue += model.GetPeopleCount(nr, nc) * IncomePerPerson;
                    }
                }
            }
            return revenue;
        }

        // Проверка радиуса действия
        public bool IsCellCovered(GameModel model, int row, int column)
        {
            // Сама клетка
            if (model.GetHasShop(row, column)) return true;
            // Вверх
            if (row > 0 && model.GetHasShop(row - 1, column)) return true;
            // Вниз
            if (row < GameModel.RowCount - 1 && model.GetHasShop(row + 1, column)) return true;
            // Лево
            if (column > 0 && model.GetHasShop(row, column - 1)) return true;
            // Право
            if (column < GameModel.ColumnCount - 1 && model.GetHasShop(row, column + 1)) return true;

            return false;
        }

        // Cчитаеv зожников среди 8 соседей вокруг клетки
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
                    if (nr >= 0 && nr < GameModel.RowCount && nc >= 0 && nc < GameModel.ColumnCount)
                    {
                        if (model.GetIsVeggie(nr, nc)) count++;
                    }
                }
            }
            return count;
        }

        public GameResult CheckGameStatus(GameModel model)
        {
            if (model.Money < 0)
            {
                return GameResult.GameOver;
            }
            if (model.TurnsPlayed >= GameModel.TargetTurns)
            {
                return GameResult.Win;
            }
            return GameResult.None;
        }
    }
}