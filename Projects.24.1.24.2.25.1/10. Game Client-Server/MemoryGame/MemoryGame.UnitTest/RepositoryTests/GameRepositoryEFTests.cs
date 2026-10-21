using MemoryGame.Common.Definitions;
using MemoryGame.Common.Dtos;
using MemoryGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.UnitTest.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTests
    {
        [TestMethod]
        public void CreateAndReadGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            if (!allUsers.Any())
            {
                return;
            }

            var selectedUser = allUsers.First();
            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
                MoveCount = 0,
            };

            // Заполняем поле парами карточек для Memory
            int value = 1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = value;
                    gameDto.States[row, column] = 0; // Hidden
                    if (column % 2 == 1)
                    {
                        value++;
                    }
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();

            var gameRepository = new GameRepositoryEF();
            int gameCount = 0;

            foreach (var user in allUsers)
            {
                var games = gameRepository.GetByUserId(user.Id);
                gameCount += games.Count();
            }

            Assert.IsTrue(gameCount > 0);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();

            var gameRepository = new GameRepositoryEF();
            GameDto game = null;

            foreach (var user in allUsers)
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
                return;
            }

            game.MoveCount++;

            // Меняем состояние первой скрытой ячейки на открытую
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (game.States[row, column] == 0) // Hidden
                    {
                        game.States[row, column] = 1; // Opened
                        goto done;
                    }
                }
            }

            done:
            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();

            var gameRepository = new GameRepositoryEF();
            GameDto game = null;

            foreach (var user in allUsers)
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
                throw new AssertFailedException("Игр нет");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
