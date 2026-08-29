using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using LightsOutGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
                { Constants.LightOn,  Constants.LightOff, Constants.LightOn,  Constants.LightOff, Constants.LightOn,  },
                { Constants.LightOff, Constants.LightOn,  Constants.LightOff, Constants.LightOn,  Constants.LightOff, },
                { Constants.LightOn,  Constants.LightOff, Constants.LightOn,  Constants.LightOff, Constants.LightOn,  },
                { Constants.LightOff, Constants.LightOn,  Constants.LightOff, Constants.LightOn,  Constants.LightOff, },
                { Constants.LightOn,  Constants.LightOff, Constants.LightOn,  Constants.LightOff, Constants.LightOn,  },
            };

            var userRepository = new UserRepositoryEF();
            var user = userRepository.GetAll().FirstOrDefault();

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
            Assert.AreEqual(gameDto.Cells[2, 2], readGameDto.Cells[2, 2]);
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
                gameCount += gameRepository.GetByUserId(user.Id).Count();
            }

            Assert.IsTrue(gameCount >= 0);
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
            int oldValue = game.Cells[0, 0];
            game.Cells[0, 0] = oldValue == Constants.LightOn ? Constants.LightOff : Constants.LightOn;

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
