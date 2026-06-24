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
        private const int IncomePerPerson = 10;  // Доход за сытого человека
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
                }
            }
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            // Если там уже есть шаурмичная, строить нельзя, ход не тратится
            if (model.GetHasShop(row, column))
            {
                return false;
            }

            // Списываем деньги за постройку
            model.Money -= GameModel.ShopCost;
            model.SetHasShop(row, column, true);
            model.TurnsPlayed++;

            // Считаем экономику за этот ход
            CalculateEconomy(model);

            return true;
        }

        private void CalculateEconomy(GameModel model)
        {
            int turnIncome = 0;
            int turnPenalty = 0;

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    int people = model.GetPeopleCount(r, c);

                    if (IsCellCovered(model, r, c))
                    {
                        turnIncome += people * IncomePerPerson;
                    }
                    else
                    {
                        turnPenalty += people * PenaltyPerPerson;
                    }
                }
            }

            model.Money += turnIncome;
            model.Money -= turnPenalty;
        }

        // Проверка радиуса действия
        private bool IsCellCovered(GameModel model, int row, int column)
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