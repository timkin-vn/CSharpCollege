using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Definitions;
using Game2048.Common.Dtos;
using Game2048.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTest.RepositoryTests
{
    public class GameEFRepositoryTests
    {
        [TestMethod]
        public void CreateGameTest()
        {
            var userRepository = new UserEFRepository();
            var users = userRepository.GetAll();
            var firstUser = users.FirstOrDefault();
            if (firstUser == null) return;

            var gameRepository = new GameEFRepository();
            var gameStart = DateTime.Now;
            var gameDto = new GameDto
            {
                UserId = firstUser.Id,
                MoveCount = 0,
                GameStart = gameStart,
            };

            var cellValues = new int[,]
            {
                { 2, 0, 0, 0 },
                { 0, 4, 0, 0 },
                { 0, 0, 2, 0 },
                { 0, 0, 0, 4 }
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = cellValues[row, column];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.IsTrue(Math.Abs((gameDto.GameStart - readGameDto.GameStart).TotalSeconds) < 1);

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(gameDto.Cells[row, column], readGameDto.Cells[row, column]);
                }
            }
        }

        [TestMethod]
        public void GetByUserTest()
        {
            var userRepository = new UserEFRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameEFRepository();
            var gameCount = 0;

            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                gameCount += games.Count();
            }

            Assert.IsTrue(gameCount > 0);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserEFRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameEFRepository();
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
                throw new Exception("Нет игр для обновления");
            }

            int targetRow = 0, targetColumn = 0;
            game.Cells[targetRow, targetColumn] += 2;
            game.MoveCount++;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Cells[targetRow, targetColumn], readGame.Cells[targetRow, targetColumn]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserEFRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameEFRepository();
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
                throw new Exception("Нет игр для удаления");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
