using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Repositories;
using Game2048.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTest.RepositoryTests
{
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUser()
        {
            var repository = new UserEFRepository();
            var userName = "user" + new Random().Next(1000);
            var user = repository.Create(userName);
            Assert.AreEqual(userName, user.Name);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            var repository = new UserEFRepository();
            var users = repository.GetAll();
            Assert.IsTrue(users.Count() > 0);
        }

        [TestMethod]
        public void GetByName()
        {
            var repository = new UserEFRepository();
            var userName = "user" + new Random().Next(1000);
            repository.Create(userName);
            var user = repository.GetByName(userName);
            Assert.IsNotNull(user);
            Assert.AreEqual(userName, user.Name);
        }
    }
}
