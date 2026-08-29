using LightsOutGame.Business.Services;
using LightsOutGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.UnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var result = userService.GetAllUsers();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var allUsers = userService.GetAllUsers();
            Assert.IsTrue(allUsers.Any());

            var selectedUser = allUsers.First();
            var sameUser = userService.GetOrCreateUser(selectedUser.Name);
            Assert.AreEqual(selectedUser.Id, sameUser.Id);
        }
    }
}
