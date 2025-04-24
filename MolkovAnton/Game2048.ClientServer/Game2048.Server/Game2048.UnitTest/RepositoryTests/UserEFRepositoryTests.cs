using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTest.RepositoryTests
{
    public class UserEFRepositoryTests
    {
        [TestMethod]
        public void CreateUser()
        {
            var repository = new UserEFRepository();
            var userName = "user" + new Random().Next(1000).ToString();
            var user = repository.Create(userName);
            Assert.AreEqual(user.Name, userName);
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
            var userName = "user" + new Random().Next(1000).ToString();
            repository.Create(userName);

            var user = repository.GetByName(userName);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Name, userName);
        }
    }
}
