using FifteenGame.ClientProxy.Services;
using FifteenGame.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.ClientUnitTests.ServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void GetAllUsers()
        {
            var service = new UserServiceProxy();
            var users = service.GetAll();
            Assert.IsTrue(users.Count() > 0);
        }

        [TestMethod]
        public void GetByName()
        {
            var service = new UserServiceProxy();
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

        [TestMethod]
        public void CreateUser()
        {
            var service = new UserServiceProxy();
            var userName = "user" + new Random().Next(100).ToString();
            var user = service.Create(userName);
            Assert.AreEqual(user.Name, userName);
        }
    }
}
