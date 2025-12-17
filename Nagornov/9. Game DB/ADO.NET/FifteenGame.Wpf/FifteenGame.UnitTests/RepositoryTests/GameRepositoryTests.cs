using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepository();
            var gameRepository = new GameRepository();

            var testUser = userRepository.GetByName("TestUserForTests");
            if (testUser == null)
            {
                testUser = userRepository.Create("TestUserForTests");
            }

            // тестовая игра
            var gameDto = new GameDto
            {
                UserId = testUser.Id,
                MoveCount = 0,
                MinesCount = 10,
                FlagsCount = 0,
                GameState = "Playing"
            };

            // Инициализируем поле 10x10
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = 0;  
                    gameDto.Revealed[row, column] = false;
                    gameDto.Mines[row, column] = false;
                }
            }

            
            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            
            Assert.IsNotNull(readGameDto);
            Assert.AreEqual(gameId, readGameDto.Id);
            Assert.AreEqual(testUser.Id, readGameDto.UserId);
            Assert.AreEqual(0, readGameDto.MoveCount);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            
            var userRepository = new UserRepository();
            var gameRepository = new GameRepository();

            var testUser = userRepository.GetByName("TestUserForTests2");
            if (testUser == null)
            {
                testUser = userRepository.Create("TestUserForTests2");
            }

           
            var games = gameRepository.GetByUserId(testUser.Id);

            Assert.IsNotNull(games);
            
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            
            var userRepository = new UserRepository();
            var gameRepository = new GameRepository();

            var testUser = userRepository.GetByName("TestUserForTests3");
            if (testUser == null)
            {
                testUser = userRepository.Create("TestUserForTests3");
            }

            var gameDto = new GameDto
            {
                UserId = testUser.Id,
                MoveCount = 5,
                MinesCount = 10,
                FlagsCount = 2,
                GameState = "Playing"
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = 0;
                    gameDto.Revealed[row, column] = false;
                    gameDto.Mines[row, column] = false;
                }
            }

            var gameId = gameRepository.Save(gameDto);
            gameDto.Id = gameId;

            
            gameDto.MoveCount = 10;
            gameRepository.Save(gameDto);

            var updatedGame = gameRepository.GetByGameId(gameId);

            
            Assert.IsNotNull(updatedGame);
            Assert.AreEqual(10, updatedGame.MoveCount);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            
            var userRepository = new UserRepository();
            var gameRepository = new GameRepository();

            var testUser = userRepository.GetByName("TestUserForTests4");
            if (testUser == null)
            {
                testUser = userRepository.Create("TestUserForTests4");
            }

            var gameDto = new GameDto
            {
                UserId = testUser.Id,
                MoveCount = 3,
                MinesCount = 10,
                FlagsCount = 1,
                GameState = "Playing"
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = 0;
                    gameDto.Revealed[row, column] = false;
                    gameDto.Mines[row, column] = false;
                }
            }

            var gameId = gameRepository.Save(gameDto);

            
            gameRepository.Remove(gameId);
            var deletedGame = gameRepository.GetByGameId(gameId);

            
            Assert.IsNull(deletedGame);
        }
    }
}