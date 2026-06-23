using SeaBattle.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaBattle.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void SaveResult_AssignsIdAndStores()
        {
            var service = new GameService(new FakeGameRepository());
            var game = service.SaveResult(42, 17, true);

            Assert.IsTrue(game.Id > 0);
            Assert.AreEqual(42, game.UserId);
            Assert.AreEqual(17, game.MoveCount);
            Assert.IsTrue(game.Won);
        }

        [TestMethod]
        public void GetHistory_ReturnsSavedGamesNewestFirst()
        {
            var service = new GameService(new FakeGameRepository());
            service.SaveResult(1, 10, false);
            service.SaveResult(1, 20, true);

            var history = service.GetHistory(1);
            Assert.AreEqual(2, history.Count);
            Assert.AreEqual(20, history[0].MoveCount);
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
