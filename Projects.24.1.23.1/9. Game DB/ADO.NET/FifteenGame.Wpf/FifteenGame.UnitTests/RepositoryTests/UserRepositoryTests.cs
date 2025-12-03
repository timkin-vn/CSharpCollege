using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            string userName = $"TestUser{new Random().Next(100)}";
            var repository = new UserRepository();
            var result = repository.Create(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var userRepository = new UserRepository();
            var result = userRepository.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userRepository = new UserRepository();
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = $"TestUser{i}";
                if (userRepository.GetByName(userName) != null)
                {
                    userCount++;
                }
            }

            Assert.IsTrue(userCount > 0);
        }
    }
}
