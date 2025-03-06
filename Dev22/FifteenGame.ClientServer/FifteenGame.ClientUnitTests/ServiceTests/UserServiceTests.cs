﻿using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.ClientUnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var service = new UserServiceProxy();
            var result = service.GetAllUsers();
            Assert.IsTrue(result.Count() > 0);
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

            var user = service.GetUserByName("NotExistingUser");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            string userName = "TestUser" + new Random().Next(100).ToString();
            var service = new UserServiceProxy();
            var result = service.GetOrCreateUser(userName);
            Assert.AreEqual(userName, result.Name);
        }
    }
}
