using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryEFTests
    {
        [TestMethod]
        public void GetAllUsersTest()
        {
   
            var repository = new UserRepositoryEF();

            repository.Create("TestUser_GetAll_" + Guid.NewGuid().ToString().Substring(0, 5));

            var result = repository.GetAll();

            Assert.IsTrue(result.Any(), "Метод GetAll должен возвращать список пользователей.");
        }

        [TestMethod]
        public void CreateUserTest()
        {

            string userName = "TestUser_" + new Random().Next(1000, 9999).ToString();
            var repository = new UserRepositoryEF();

            var result = repository.Create(userName);

            Assert.IsNotNull(result);
            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            var repository = new UserRepositoryEF();
            string uniqueName = "FindMe_" + Guid.NewGuid().ToString().Substring(0, 8);

            repository.Create(uniqueName);

            var foundUser = repository.GetByName(uniqueName);

            Assert.IsNotNull(foundUser, $"Пользователь с именем {uniqueName} должен быть найден в БД.");
            Assert.AreEqual(uniqueName, foundUser.Name);
        }

        [TestMethod]
        public void GetUserByIdTest()
        {
            var repository = new UserRepositoryEF();
            string userName = "TestUser_GetById_" + Guid.NewGuid().ToString().Substring(0, 5);
            var createdUser = repository.Create(userName);

            var selectedUser = repository.GetById(createdUser.Id);

            Assert.IsNotNull(selectedUser, "Пользователь должен быть найден по существующему Id.");
            Assert.AreEqual(createdUser.Name, selectedUser.Name);
        }
    }
}