using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 5;
        public const int ColumnCount = 5;
        public const int InitialMoney = 1000;
        public const int ShopCost = 300;
        public const int TargetTurns = 10; // Сколько ходов нужно продержаться для победы

        // Двумерные массивы для хранения состояния поля
        private int[,] _peopleCount = new int[RowCount, ColumnCount];
        private bool[,] _hasShop = new bool[RowCount, ColumnCount];

        public int GetPeopleCount(int row, int column) => _peopleCount[row, column];
        public void SetPeopleCount(int row, int column, int value) => _peopleCount[row, column] = value;

        public bool GetHasShop(int row, int column) => _hasShop[row, column];
        public void SetHasShop(int row, int column, bool value) => _hasShop[row, column] = value;

        // Текущие показатели игры
        public int Money { get; internal set; }
        public int TurnsPlayed { get; internal set; }
    }
}