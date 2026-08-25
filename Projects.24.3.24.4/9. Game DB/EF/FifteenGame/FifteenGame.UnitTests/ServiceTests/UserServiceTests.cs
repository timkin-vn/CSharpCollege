using FifteenGame.Business.Services;
using FifteenGame.DataAccess.EF.Repositories;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTests.ServiceTests
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
        public void GetUserByNameTest()
        {
            var userService = new UserService(new UserRepositoryEF());
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = "TestUser" + i.ToString();
                if (userService.GetUserByName(userName) != null)
                {
                    userCount++;
                }
            }

            Assert.IsTrue(userCount > 0);
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

            for (int i = 0; i < 100; i++)
            {
                string userName = "TestUser" + i.ToString();
                if (allUsers.FirstOrDefault(u => u.Name == userName) != null)
                {
                    continue;
                }

                var createdUser = userService.GetOrCreateUser(selectedUser.Name);
                Assert.IsNotNull(createdUser);
                Assert.AreNotEqual(createdUser.Id, 0);
                break;
            }
        }
    }
}
