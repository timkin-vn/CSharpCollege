using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTests
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var gameCells = new[,]
            {
                { true,  false, true,  false, true,  },
                { false, true,  false, true,  false, },
                { true,  false, true,  false, true,  },
                { false, true,  false, true,  false, },
                { true,  false, true,  false, true,  },
            };

            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            var user = allUsers.FirstOrDefault();

            if (user == null)
            {
                return;
            }

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = user.Id,
                MoveCount = 0,
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

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.Cells[0, 0], readGameDto.Cells[0, 0]);
            Assert.AreEqual(gameDto.Cells[2, 3], readGameDto.Cells[2, 3]);
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
            game.Cells[0, 0] = !game.Cells[0, 0];

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Cells[0, 0], readGame.Cells[0, 0]);
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
                throw new AssertFailedException("Нет игр");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
