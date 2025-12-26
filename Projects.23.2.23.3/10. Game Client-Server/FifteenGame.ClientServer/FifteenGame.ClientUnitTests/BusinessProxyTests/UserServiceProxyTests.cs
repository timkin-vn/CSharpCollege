using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.ClientUnitTests.BusinessProxyTests
{
    [TestClass]
    public class UserServiceProxyTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var service = new UserServiceProxy();
            var result = service.GetAllUsers();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateUserTest()
        {
            string userName = "TestUser" + new Random().Next(100).ToString();
            var service = new UserServiceProxy();
            var result = service.GetOrCreateUser(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var service = new UserServiceProxy();
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = "TestUser" + i.ToString();
                if (service.GetUserByName(userName) != null)
                {
                    userCount++;
                }
            }

            Assert.IsTrue(userCount > 0);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var service = new UserServiceProxy();
            var users = service.GetAllUsers();
            var userCount = 0;
            foreach (var user in users)
            {
                if (service.GetUserById(user.Id) != null)
                {
                    userCount++;
                }
            }

            Assert.AreEqual(users.Count(), userCount);
        }
    }
}
