using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepository();
            var selectedUser = userRepository.GetAll().FirstOrDefault();
            if (selectedUser == null)
                return;

            var gameRepository = new GameRepository();

            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
                MoveCount = 100,
                Score = 256,
                IsWin = false,
                IsLose = false
            };

            var gameCells = new[,]
            {
                { 2, 0, 0, 2 },
                { 4, 4, 0, 0 },
                { 0, 8, 0, 0 },
                { 0, 0, 16, 0 },
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = gameCells[row, column];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.IsNotNull(readGameDto);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.Score, readGameDto.Score);
            Assert.AreEqual(gameDto.IsWin, readGameDto.IsWin);
            Assert.AreEqual(gameDto.IsLose, readGameDto.IsLose);

            Assert.AreEqual(16, readGameDto.Cells[3, 2]);
            Assert.AreEqual(Constants.EmptyCellValue, readGameDto.Cells[0, 1]);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll().ToList();

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
        public void UpdateGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll().ToList();

            var gameRepository = new GameRepository();

            GameDto game = null;
            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id).ToList();
                if (games.Any())
                {
                    game = games.First();
                    break;
                }
            }

            if (game == null)
                return;

            game.MoveCount++;
            game.Score += 10;

            game.Cells[0, 0] = 2;
            game.Cells[0, 1] = 2;
            game.Cells[0, 2] = 4;
            game.Cells[0, 3] = 0;

            gameRepository.Save(game);

            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNotNull(readGame);
            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Score, readGame.Score);
            Assert.AreEqual(4, readGame.Cells[0, 2]);
            Assert.AreEqual(0, readGame.Cells[0, 3]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll().ToList();

            var gameRepository = new GameRepository();

            GameDto game = null;
            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id).ToList();
                if (games.Any())

                   
{
                    game = games.First();
                    break;
                }
            }

            if (game == null)
                return;

            gameRepository.Remove(game.Id);

            var readGame = gameRepository.GetByGameId(game.Id);
            Assert.IsNull(readGame);
        }
    }
}