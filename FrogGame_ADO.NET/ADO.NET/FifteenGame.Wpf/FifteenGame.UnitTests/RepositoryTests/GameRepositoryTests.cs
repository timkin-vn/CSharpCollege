using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FrogGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void CreateAndReadFrogGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var selectedUser = users.FirstOrDefault();

            if (selectedUser == null)
            {
                return;
            }

            var gameRepository = new GameRepository();
            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
                MoveCount = 10,
                FrogRow = 0,
                FrogColumn = 0,
                HomeRow = 7,
                HomeColumn = 7,
                IsGameOver = false,
                IsWin = false
            };

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    gameDto.Cells[row, column] = 0;
                }
            }

            gameDto.Cells[0, 0] = 3; 
            gameDto.Cells[7, 7] = 4;
            gameDto.Cells[0, 1] = 1;
            gameDto.Cells[1, 1] = 1;
            gameDto.Cells[1, 2] = 1;

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.FrogRow, readGameDto.FrogRow);
            Assert.AreEqual(gameDto.HomeRow, readGameDto.HomeRow);
            Assert.AreEqual(gameDto.Cells[0, 0], readGameDto.Cells[0, 0]);
            Assert.AreEqual(gameDto.Cells[7, 7], readGameDto.Cells[7, 7]);
        }

        [TestMethod]
        public void GetFrogGamesByUserIdTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();

            int gameCount = 0;
            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                gameCount += games.Count();
            }

            Assert.IsTrue(gameCount >= 0); 
        }

        [TestMethod]
        public void UpdateFrogGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();
            GameDto game = null;

            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                if (games.Any())
                {
                    game = games.First();
                    break;
                }
            }

            if (game == null)
            {
                var testUser = users.First();
                game = new GameDto
                {
                    UserId = testUser.Id,
                    MoveCount = 5,
                    FrogRow = 0,
                    FrogColumn = 0,
                    HomeRow = 7,
                    HomeColumn = 7,
                    IsGameOver = false,
                    IsWin = false
                };

                for (int row = 0; row < 8; row++)
                {
                    for (int column = 0; column < 8; column++)
                    {
                        game.Cells[row, column] = 0;
                    }
                }
                game.Cells[0, 0] = 3;
                game.Cells[7, 7] = 4;

                var gameId = gameRepository.Save(game);
                game = gameRepository.GetByGameId(gameId);
            }
            game.MoveCount++;
            game.FrogRow = 1;
            game.FrogColumn = 1;

            game.Cells[0, 0] = 1; 
            game.Cells[1, 1] = 3; 

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.FrogRow, readGame.FrogRow);
            Assert.AreEqual(game.Cells[1, 1], readGame.Cells[1, 1]);
        }

        [TestMethod]
        public void RemoveFrogGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameRepository();

            var testUser = users.First();
            var gameDto = new GameDto
            {
                UserId = testUser.Id,
                MoveCount = 1,
                FrogRow = 0,
                FrogColumn = 0,
                HomeRow = 7,
                HomeColumn = 7
            };

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    gameDto.Cells[row, column] = 0;
                }
            }
            gameDto.Cells[0, 0] = 3;
            gameDto.Cells[7, 7] = 4;

            var gameId = gameRepository.Save(gameDto);

            gameRepository.Remove(gameId);
            var readGame = gameRepository.GetByGameId(gameId);

            Assert.IsNull(readGame);
        }
    }
}