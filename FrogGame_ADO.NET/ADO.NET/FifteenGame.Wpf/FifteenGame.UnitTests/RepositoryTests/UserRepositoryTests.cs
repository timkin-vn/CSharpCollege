using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FrogGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            string userName = $"FrogPlayer{new Random().Next(1000)}";
            var repository = new UserRepository();
            var result = repository.Create(userName);

            Assert.AreEqual(userName, result.Name);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var userRepository = new UserRepository();
            var result = userRepository.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserByNameTest()
        {
            string userName = $"TestFrogUser{new Random().Next(1000)}";
            var userRepository = new UserRepository();
            var createdUser = userRepository.Create(userName);
            var foundUser = userRepository.GetByName(userName);

            Assert.IsNotNull(foundUser);
            Assert.AreEqual(userName, foundUser.Name);
            Assert.AreEqual(createdUser.Id, foundUser.Id);
        }
    }
}