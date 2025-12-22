using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Common.Dto;
using System;

namespace Minesweeper.Tests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void Field_Constructor_ShouldInitializeCorrectly()
        {

            var field = new Business.Core.Field(10, 15);


            Assert.AreEqual(10, field.Size);
            Assert.AreEqual(15, field.MineCount);
            Assert.IsFalse(field.GameOver);
            Assert.IsFalse(field.GameWon);
        }

        [TestMethod]
        public void PlaceMines_ShouldPlaceCorrectNumberOfMines()
        {

            var field = new Business.Core.Field(10, 15);
            int mineCount = 0;


            for (int i = 0; i < field.Size; i++)
            {
                for (int j = 0; j < field.Size; j++)
                {
                    if (field.GetMine(i, j))
                        mineCount++;
                }
            }


            Assert.AreEqual(15, mineCount);
        }

        [TestMethod]
        public void RevealCell_OnMine_ShouldSetGameOver()
        {

            var field = new Business.Core.Field(10, 15);
            int mineRow = -1, mineCol = -1;


            for (int i = 0; i < field.Size && mineRow == -1; i++)
            {
                for (int j = 0; j < field.Size && mineCol == -1; j++)
                {
                    if (field.GetMine(i, j))
                    {
                        mineRow = i;
                        mineCol = j;
                    }
                }
            }


            field.RevealCell(mineRow, mineCol);


            Assert.IsTrue(field.GameOver);
        }

        [TestMethod]
        public void ToggleFlag_ShouldToggleFlagState()
        {

            var field = new Business.Core.Field(10, 15);
            int row = 0, col = 0;


            while (field.GetMine(row, col))
            {
                col++;
                if (col >= field.Size)
                {
                    row++;
                    col = 0;
                }
            }

            field.ToggleFlag(row, col);

            field.ToggleFlag(row, col);
        }
    }

    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_Properties_ShouldBeSetCorrectly()
        {
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                CreatedAt = DateTime.UtcNow,
                TotalGamesPlayed = 10,
                GamesWon = 5
            };

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("testuser", user.Username);
            Assert.AreEqual(10, user.TotalGamesPlayed);
            Assert.AreEqual(5, user.GamesWon);
        }
    }

    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        public void GameState_Properties_ShouldBeSetCorrectly()
        {
            var gameState = new GameState
            {
                Id = 1,
                UserId = 123,
                GameData = "test data",
                IsGameOver = false,
                IsGameWon = false,
                PlayTime = TimeSpan.FromMinutes(5)
            };

            Assert.AreEqual(1, gameState.Id);
            Assert.AreEqual(123, gameState.UserId);
            Assert.AreEqual(false, gameState.IsGameOver);
            Assert.AreEqual(false, gameState.IsGameWon);
            Assert.AreEqual(TimeSpan.FromMinutes(5), gameState.PlayTime);
        }
    }
}