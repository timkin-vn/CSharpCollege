using FifteenGame.DataAccess.EF.Repositories;
using FifteenGame.DataAccess.Repoistories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTest.RepositoryTests
{
    [TestClass]
    public class UserEFRepositoryTests
    {
        [TestMethod]
        public void CreateUser()
        {
            var repository = new UserEFRepository();
            var userName = "user" + new Random().Next(100).ToString();
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
            var count = 0;
            for (int i = 0; i < 100; i++)
            {
                var userName = "user" + i.ToString();
                if (repository.GetByName(userName) != null)
                {
                    count++;
                }
            }

            Assert.IsTrue(count > 0);
        }
    }
}
