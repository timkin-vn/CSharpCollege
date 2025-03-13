using FifteenGame.Business.Services;
using FifteenGame.DataAccess.Repoistories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTest.BusinessTests
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void CreateUser()
        {
            var service = new UserService();
            var userName = "user" + new Random().Next(100).ToString();
            var user = service.Create(userName);
            Assert.AreEqual(user.Name, userName);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            var service = new UserService();
            var users = service.GetAll();
            Assert.IsTrue(users.Count() > 0);
        }

        [TestMethod]
        public void GetByName()
        {
            var service = new UserService();
            var count = 0;
            for (int i = 0; i < 100; i++)
            {
                var userName = "user" + i.ToString();
                if (service.GetByName(userName) != null)
                {
                    count++;
                }
            }

            Assert.IsTrue(count > 0);
        }
    }
}
