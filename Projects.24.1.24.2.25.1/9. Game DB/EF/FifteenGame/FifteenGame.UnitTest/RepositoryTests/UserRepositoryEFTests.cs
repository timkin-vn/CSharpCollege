using FifteenGame.DataAccess.EF.Repositories;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTest.RepositoryTests
{
    [TestClass]
    public class UserRepositoryEFTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var userRepository = new UserRepositoryEF();
            var result = userRepository.GetAll();

            Assert.IsTrue(result.Any());
        }

        // TDD Test Driven Design
        [TestMethod]
        public void CreateUserTest()
        {
            var userRepository = new UserRepositoryEF();
            string userName = "TestUser" + new Random().Next(100).ToString();
            var result = userRepository.Create(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userRepository = new UserRepositoryEF();
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = "TestUser" + i.ToString();
                if (userRepository.GetByName(userName) != null)
                {
                    userCount++;
                }
            }

            Assert.IsTrue(userCount > 0);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            if (!allUsers.Any())
            {
                return;
            }

            var user = allUsers.First();
            var selectedUser = userRepository.GetById(user.Id);
            Assert.AreEqual(user.Name, selectedUser.Name);
        }
    }
}
