using LightsOutGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryEFTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
            var repository = new UserRepositoryEF();
            var result = repository.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateUserTest()
        {
            string userName = "TestUser" + new Random().Next(100).ToString();
            var repository = new UserRepositoryEF();
            var result = repository.Create(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var repository = new UserRepositoryEF();
            var allUsers = repository.GetAll();
            if (!allUsers.Any())
            {
                return;
            }

            var user = allUsers.First();
            var selectedUser = repository.GetById(user.Id);
            Assert.AreEqual(user.Name, selectedUser.Name);
        }
    }
}
