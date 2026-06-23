using Minesweeper.Business;
using Minesweeper.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void GetByUserId_CreatesGameWhenNoneExists()
        {
            var service = new GameService(new FakeGameRepository());
            var game = service.GetByUserId(42);

            Assert.IsNotNull(game);
            Assert.AreEqual(42, game.UserId);
            Assert.IsTrue(game.Id > 0);
        }

        [TestMethod]
        public void GetByUserId_ReturnsSameGameOnSecondCall()
        {
            var service = new GameService(new FakeGameRepository());
            var first = service.GetByUserId(1);
            var second = service.GetByUserId(1);
            Assert.AreEqual(first.Id, second.Id);
        }

        [TestMethod]
        public void Apply_RevealMarksCellRevealed()
        {
            var service = new GameService(new FakeGameRepository());
            var game = service.Apply(7, 0, 0, CellAction.Reveal);

            Assert.IsTrue(game.Field[0, 0].IsRevealed);
            Assert.IsTrue(game.MoveCount > 0);
        }

        [TestMethod]
        public void Login_CreatesUserThenReusesIt()
        {
            var service = new UserService(new FakeUserRepository());
            var u1 = service.Login("artem");
            var u2 = service.Login("artem");
            Assert.AreEqual(u1.Id, u2.Id);
            Assert.AreEqual("artem", u1.Name);
        }
    }
}
