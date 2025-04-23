using FifteenGame.DataAccess.EF.Repositories;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserEFRepositoryTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            string userName = "TestUser" + new Random().Next(100).ToString();
            var repository = new UserEFRepository();
            var result = repository.Create(userName);
            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var repository = new UserEFRepository();
            var result = repository.GetAll();
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var repository = new UserEFRepository();
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
