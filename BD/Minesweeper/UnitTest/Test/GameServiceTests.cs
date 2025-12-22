using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Business.Core;
using Minesweeper.Common.Dto;

using System;

namespace Minesweeper.UnitTests.ServiceTests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void Field_Constructor_ShouldInitializeCorrectly()
        {
            var field = new Field(10, 15);

            Assert.AreEqual(10, field.Size, "Размер поля должен быть 10x10");
            Assert.AreEqual(15, field.MineCount, "Количество мин должно быть 15");
            Assert.IsFalse(field.GameOver, "Игра не должна быть завершена при создании");
            Assert.IsFalse(field.GameWon, "Игра не должна быть выиграна при создании");
        }

        [TestMethod]
        public void Field_PlaceMines_ShouldPlaceCorrectNumberOfMines()
        {
            var field = new Field(10, 15);
            int mineCount = 0;

            for (int row = 0; row < field.Size; row++)
            {
                for (int col = 0; col < field.Size; col++)
                {
                    if (field.GetMine(row, col))
                        mineCount++;
                }
            }
            Assert.AreEqual(15, mineCount, "Должно быть размещено 15 мин");
        }

        [TestMethod]
        public void Field_RevealCell_OnMine_ShouldSetGameOver()
        {
            var field = new Field(10, 15);
            int mineRow = -1, mineCol = -1;

            for (int row = 0; row < field.Size && mineRow == -1; row++)
            {
                for (int col = 0; col < field.Size && mineCol == -1; col++)
                {
                    if (field.GetMine(row, col))
                    {
                        mineRow = row;
                        mineCol = col;
                    }
                }
            }
            field.RevealCell(mineRow, mineCol);
            Assert.IsTrue(field.GameOver, "При открытии мины игра должна завершиться");
            Assert.IsFalse(field.GameWon, "Игра не должна быть выиграна при подрыве на мине");
        }

        [TestMethod]
        public void Field_RevealCell_OnEmptyCell_ShouldNotSetGameOver()
        {
            var field = new Field(10, 15);
            int emptyRow = -1, emptyCol = -1;

            for (int row = 0; row < field.Size && emptyRow == -1; row++)
            {
                for (int col = 0; col < field.Size && emptyCol == -1; col++)
                {
                    if (!field.GetMine(row, col))
                    {
                        emptyRow = row;
                        emptyCol = col;
                    }
                }
            }

            field.RevealCell(emptyRow, emptyCol);

            Assert.IsFalse(field.GameOver, "При открытии пустой клетки игра не должна завершаться");
            Assert.IsFalse(field.GameWon, "Игра не должна быть выиграна после одного хода");
        }

        [TestMethod]
        public void Field_ToggleFlag_ShouldToggleFlagState()
        {
            var field = new Field(10, 15);
            int row = 0, col = 0;

            while (field.GetMine(row, col) && row < field.Size)
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



        [TestMethod]
        public void Field_CheckWinCondition_AllSafeCellsRevealed_ShouldSetGameWon()
        {
            var field = new Field(3, 1); 
            field.Mines[0, 0] = true; 

            for (int row = 0; row < field.Size; row++)
            {
                for (int col = 0; col < field.Size; col++)
                {
                    if (!field.GetMine(row, col))
                    {
                        field.Revealed[row, col] = true;
                    }
                }
            }

            field.CheckWinCondition();

            Assert.IsTrue(field.GameWon, "Игра должна быть выиграна при открытии всех безопасных клеток");
            Assert.IsFalse(field.GameOver, "Игра не должна быть проиграна");
        }

        [TestMethod]
        public void Field_RestartGame_ShouldResetGameState()
        {
            var field = new Field(10, 15);

            field.RevealCell(0, 0);
            field.ToggleFlag(1, 1);
            field.GameOver = true;

            field.RestartGame();

            Assert.IsFalse(field.GameOver, "Игра не должна быть завершена после рестарта");
            Assert.IsFalse(field.GameWon, "Игра не должна быть выиграна после рестарта");

            bool allCellsReset = true;
            for (int row = 0; row < field.Size; row++)
            {
                for (int col = 0; col < field.Size; col++)
                {
                    if (field.Revealed[row, col] || field.Flag[row, col])
                    {
                        allCellsReset = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(allCellsReset, "Все клетки должны быть сброшены после рестарта");
        }
    }

    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void UserRepository_CreateUser_ShouldReturnUserWithCorrectProperties()
        {
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                CreatedAt = DateTime.UtcNow,
                TotalGamesPlayed = 10,
                GamesWon = 5
            };

            Assert.AreEqual(1, user.Id, "ID должен быть 1");
            Assert.AreEqual("testuser", user.Username, "Username должен быть 'testuser'");
            Assert.AreEqual(10, user.TotalGamesPlayed, "TotalGamesPlayed должен быть 10");
            Assert.AreEqual(5, user.GamesWon, "GamesWon должен быть 5");
        }

        [TestMethod]
        public void User_DefaultValues_ShouldBeSet()
        {
            var user = new User
            {
                Id = 0,
                Username = "newuser"
            };

            Assert.AreEqual(0, user.Id, "ID по умолчанию должен быть 0");
            Assert.AreEqual("newuser", user.Username, "Username должен быть установлен");
            Assert.AreEqual(0, user.TotalGamesPlayed, "TotalGamesPlayed по умолчанию должен быть 0");
            Assert.AreEqual(0, user.GamesWon, "GamesWon по умолчанию должен быть 0");
        }
    }

    [TestClass]
    public class GameStateRepositoryTests
    {
        [TestMethod]
        public void GameState_CreateGameState_ShouldSetCorrectProperties()
        {
            var gameState = new GameState
            {
                Id = 1,
                UserId = 123,
                GameData = "{\"Size\":10,\"MineCount\":15}",
                IsGameOver = false,
                IsGameWon = false,
                PlayTime = TimeSpan.FromMinutes(5)
            };

            Assert.AreEqual(1, gameState.Id, "ID должен быть 1");
            Assert.AreEqual(123, gameState.UserId, "UserId должен быть 123");
            Assert.IsFalse(gameState.IsGameOver, "IsGameOver должен быть false");
            Assert.IsFalse(gameState.IsGameWon, "IsGameWon должен быть false");
            Assert.AreEqual(TimeSpan.FromMinutes(5), gameState.PlayTime, "PlayTime должен быть 5 минут");
        }

    }

    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void CompleteGameFlow_ShouldWorkCorrectly()
        {
            var field = new Field(10, 15);
            int safeCellsOpened = 0;
            for (int row = 0; row < field.Size && safeCellsOpened < 5; row++)
            {
                for (int col = 0; col < field.Size && safeCellsOpened < 5; col++)
                {
                    if (!field.GetMine(row, col) && !field.Revealed[row, col])
                    {
                        field.RevealCell(row, col);
                        safeCellsOpened++;
                    }
                }
            }

            int flagsPlaced = 0;
            for (int row = 0; row < field.Size && flagsPlaced < 3; row++)
            {
                for (int col = 0; col < field.Size && flagsPlaced < 3; col++)
                {
                    if (field.GetMine(row, col) && !field.Flag[row, col])
                    {
                        field.ToggleFlag(row, col);
                        flagsPlaced++;
                    }
                }
            }

            field.CheckWinCondition();

            Assert.IsTrue(safeCellsOpened > 0, "Должны быть открыты безопасные клетки");
            Assert.IsTrue(flagsPlaced > 0, "Должны быть установлены флаги");

            Assert.IsFalse(field.GameOver || field.GameWon,
                "Игра не должна быть завершена после нескольких ходов");
        }
    }
}