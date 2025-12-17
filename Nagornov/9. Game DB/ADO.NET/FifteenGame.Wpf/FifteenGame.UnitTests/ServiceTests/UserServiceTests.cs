using FifteenGame.Business.Services;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.UnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var userService = new UserService(new UserRepository());
            var result = userService.GetAllUsers();
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userService = new UserService(new UserRepository());
            var allUsers = userService.GetAllUsers();
            Assert.IsTrue(allUsers.Any());

            var selectedUser = allUsers.First();
            var gotUser = userService.GetUserByName(selectedUser.Name); 
            Assert.AreEqual(selectedUser.Id, gotUser.Id);
        }

        [TestMethod]
        public void GetOrCreateUserTest()
        {
            var userService = new UserService(new UserRepository());
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