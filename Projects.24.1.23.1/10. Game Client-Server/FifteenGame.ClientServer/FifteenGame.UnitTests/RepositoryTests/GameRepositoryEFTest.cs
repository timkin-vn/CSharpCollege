using FifteenGame.Common.Dto;
using FifteenGame.DataAcces.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTest
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();
            var selectedUser = users.FirstOrDefault();
            if (selectedUser == null) return;

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
                MatchesCount = 0,
                IsFinished = false,
            };

            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                    gameDto.Cells[row, col] = (row + col) % 5 + 1;

            var gameId = gameRepository.Save(gameDto);
            var readGame = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MatchesCount, readGame.MatchesCount);
            Assert.AreEqual(gameDto.UserId, readGame.UserId);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();
            var gameRepository = new GameRepositoryEF();

            var game = users.SelectMany(u => gameRepository.GetByUserId(u.Id)).FirstOrDefault();
            if (game == null) return;

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);
            Assert.IsNull(readGame);
        }
    }
}