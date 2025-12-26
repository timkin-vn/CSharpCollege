using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonogram.Common.Definitions;
using Nonogram.Common.Dtos;
using Nonogram.DataAccess.Repositories;
using System.Linq;

namespace Nonogram.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void CreateAndReadTest()
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
                MistakesCount = 0,
            };

            for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
            {
                for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = Common.Definitions.Constants.EmptyCell;
                }
            }

            gameDto.Cells[0, 0] = Common.Definitions.Constants.FilledCell;
            gameDto.Cells[7, 7] = Common.Definitions.Constants.CrossedCell;

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MistakesCount, readGameDto.MistakesCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(Common.Definitions.Constants.FilledCell, readGameDto.Cells[0, 0]);
            Assert.AreEqual(Common.Definitions.Constants.CrossedCell, readGameDto.Cells[7, 7]);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
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

            Assert.IsTrue(gameCount > 0);
        }

        [TestMethod]
        public void UpdateGameTest()
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
                return;
            }

            game.MistakesCount++;
            game.Cells[0, 0] = Common.Definitions.Constants.CrossedCell;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MistakesCount, readGame.MistakesCount);
            Assert.AreEqual(Common.Definitions.Constants.CrossedCell, readGame.Cells[0, 0]);
        }

        [TestMethod]
        public void RemoveGameTest()
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
                return;
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}