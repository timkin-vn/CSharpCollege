using Игра.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Игра.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryEFTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            string userName = $"TestUser{new Random().Next(100)}";
            var repository = new UserRepositoryEF();
            var result = repository.Create(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var userRepository = new UserRepositoryEF();
            var result = userRepository.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var userRepository = new UserRepositoryEF();
            int userCount = 0;
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