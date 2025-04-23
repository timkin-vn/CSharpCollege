using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            string userName = "TestUser" + new Random().Next(100).ToString();
            var repository = new UserRepository();
            var result = repository.Create(userName);
            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var repository = new UserRepository();
            var result = repository.GetAll();
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var repository = new UserRepository();
            var userCount = 0;
            for (int i = 0; i < 100; i++)
            {
                string userName = "TestUser" + i.ToString();
                if (repository.GetByName(userName) != null)
                {
                    userCount++;
                }
            }

            Assert.IsTrue(userCount > 0);
        }
    }
}
