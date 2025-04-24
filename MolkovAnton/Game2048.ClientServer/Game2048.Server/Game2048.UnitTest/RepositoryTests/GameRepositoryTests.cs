using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Definitions;
using Game2048.Common.Dtos;
using Game2048.Common.Repositories;
using Game2048.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTest.RepositoryTests
{
    public class GameRepositoryTests
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

            var initialCells = new int[,]
            {
                { 2, 0, 0, 0 },
                { 0, 4, 0, 0 },
                { 0, 0, 2, 0 },
                { 0, 0, 0, 4 }
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    gameDto.Cells[row, col] = initialCells[row, col];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.IsTrue(Math.Abs((gameDto.GameStart - readGameDto.GameStart).TotalSeconds) < 1);

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    Assert.AreEqual(gameDto.Cells[row, col], readGameDto.Cells[row, col]);
                }
            }
        }

        [TestMethod]
        public void GetByUserTest()
        {
            var userRepository = new UserEFRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameEFRepository();
            int gameCount = 0;

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

            if (game == null) throw new Exception("Нет доступных игр");

            int row = 0, col = 0;
            if (game.Cells[row, col] == 0)
                game.Cells[row, col] = 2;
            else
                game.Cells[row, col] *= 2;

            game.MoveCount++;

            gameRepository.Save(game);
            var updatedGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, updatedGame.MoveCount);
            Assert.AreEqual(game.Cells[row, col], updatedGame.Cells[row, col]);
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

            if (game == null) throw new Exception("Нет игр для удаления");

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
