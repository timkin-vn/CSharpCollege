using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nonogram.Business.Services;
using Nonogram.Common.Definitions;
using Nonogram.DataAccess.Repositories;
using System.Linq;

namespace Nonogram.UnitTests.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void MakeMoveTest()
        {
            var gameService = new GameService(new GameRepository());
            var userService = new UserService(new UserRepository());

            var user = userService.GetOrCreateUser("TestUser");
            var game = gameService.GetByUserId(user.Id);

            var initialMistakes = game.MistakesCount;

            // Попробуем сделать ход в правильную клетку (из решения)
            var moveResult = gameService.MakeMove(game.Id, 0, 9);
            Assert.IsNotNull(moveResult);

            // Проверим, что ошибок не добавилось
            Assert.IsTrue(moveResult.MistakesCount <= initialMistakes + 1);
        }

        [TestMethod]
        public void IsGameWonTest()
        {
            var gameService = new GameService(new GameRepository());
            var userService = new UserService(new UserRepository());

            var user = userService.GetOrCreateUser("TestUser");
            var game = gameService.GetByUserId(user.Id);

            // Новая игра не должна быть выиграна
            Assert.IsFalse(gameService.IsGameWon(game.Id));
        }

        [TestMethod]
        public void GetByUserIdTest()
        {
            var gameService = new GameService(new GameRepository());
            var userService = new UserService(new UserRepository());

            var user = userService.GetOrCreateUser("TestUser");
            var game = gameService.GetByUserId(user.Id);

            Assert.IsNotNull(game);
            Assert.AreEqual(user.Id, game.UserId);
            Assert.IsTrue(game.RowClues.Count > 0);
            Assert.IsTrue(game.ColumnClues.Count > 0);
        }
    }
}