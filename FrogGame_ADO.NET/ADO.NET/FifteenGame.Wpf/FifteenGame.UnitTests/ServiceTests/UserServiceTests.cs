using FifteenGame.Business.Services;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FrogGame.UnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserService(new UserRepository());
            var result = userService.GetAllUsers();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userService = new UserService(new UserRepository());
            string testName = $"FrogTestUser{System.DateTime.Now.Ticks}";
            var createdUser = userService.GetOrCreateUser(testName);

            var foundUser = userService.GetUserByName(testName);

            Assert.IsNotNull(foundUser);
            Assert.AreEqual(testName, foundUser.Name);
        }

        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserService(new UserRepository());

            string newUserName = $"NewFrogPlayer{System.DateTime.Now.Ticks}";
            var newUser = userService.GetOrCreateUser(newUserName);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(newUserName, newUser.Name);
            Assert.IsTrue(newUser.Id > 0);

            var existingUser = userService.GetOrCreateUser(newUserName);
            Assert.AreEqual(newUser.Id, existingUser.Id);
        }
    }
}