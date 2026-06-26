using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _peopleCount = new int[Constants.RowCount, Constants.ColumnCount];
        private bool[,] _hasShop = new bool[Constants.RowCount, Constants.ColumnCount];
        private bool[,] _isVeggie = new bool[Constants.RowCount, Constants.ColumnCount];
        private bool[,] _isRevealed = new bool[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public int Money { get; set; }

        public int TurnsPlayed { get; set; }

        public int GetPeopleCount(int row, int column) => _peopleCount[row, column];
        public void SetPeopleCount(int row, int column, int value) => _peopleCount[row, column] = value;

        public bool GetHasShop(int row, int column) => _hasShop[row, column];
        public void SetHasShop(int row, int column, bool value) => _hasShop[row, column] = value;

        public bool GetIsVeggie(int row, int column) => _isVeggie[row, column];
        public void SetIsVeggie(int row, int column, bool value) => _isVeggie[row, column] = value;

        public bool GetIsRevealed(int row, int column) => _isRevealed[row, column];
        public void SetIsRevealed(int row, int column, bool value) => _isRevealed[row, column] = value;
    }
}
