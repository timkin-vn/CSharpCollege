using FifteenGame.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTest.BusinessTests
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void TestGetAndDeleteGame()
        {
            var userService = new UserService();
            var users = userService.GetAll();
            if (!users.Any())
            {
                throw new Exception("Нет пользователей");
            }

            var gameService = new GameService();

            var firstUser = users.First();
            var game = gameService.GetByUserId(firstUser.Id);

            Assert.IsNotNull(game);

            gameService.RemoveGame(game.GameId);
            game = gameService.GetByGameId(game.GameId);

            Assert.IsNull(game);

            var lastUser = users.Last();
            game = gameService.GetByUserId(lastUser.Id);

            Assert.IsNotNull(game);

            gameService.RemoveGame(game.GameId);
            game = gameService.GetByGameId(game.GameId);

            Assert.IsNull(game);
        }
    }
}
