using Игра.BusinessProxy.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Игра.ClientUnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserServiceProxy();
            var result = userService.GetAllUsers();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userService = new UserServiceProxy();
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = $"TestUser{i}";
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
            var userService = new UserServiceProxy();
            var allUsers = userService.GetAllUsers();
            Assert.IsTrue(allUsers.Any());

            var selectedUser = allUsers.First();
            var gotUser = userService.GetOrCreateUser(selectedUser.Name);
            Assert.AreEqual(selectedUser.Id, gotUser.Id);

            for (int i = 0; i < 100; i++)
            {
                string userName = $"TestUser{i}";
                if (allUsers.FirstOrDefault(u => u.Name == userName) == null)
                {
                    var createdUser = userService.GetOrCreateUser(userName);
                    Assert.IsNotNull(createdUser);
                    Assert.AreNotEqual(createdUser.Id, 0);
                    break;
                }
            }
        }
    }
}
