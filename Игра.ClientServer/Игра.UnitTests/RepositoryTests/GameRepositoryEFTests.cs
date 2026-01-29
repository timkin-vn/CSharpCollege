using Игра.Common.Definitions;
using Игра.Common.Dtos;
using Игра.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Игра.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTests
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();
            var selectedUser = users.FirstOrDefault();

            Assert.IsNotNull(selectedUser, "В БД отсутствуют пользователи");

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
            };

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    gameDto.Cells[r, c] = (r + c) % 2;
                }
            }

            int id = gameRepository.Save(gameDto);

            var read = gameRepository.GetByGameId(id);

            Assert.IsNotNull(read);
            Assert.AreEqual(gameDto.UserId, read.UserId);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepositoryEF();
            var gameRepository = new GameRepositoryEF();

            var users = userRepository.GetAll();

            int totalGames = users.Sum(u => gameRepository.GetByUserId(u.Id).Count());

            Assert.IsTrue(totalGames >= 0);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var gameRepository = new GameRepositoryEF();

            var user = userRepository.GetAll().FirstOrDefault();
            Assert.IsNotNull(user);

            var game = new GameDto
            {
                UserId = user.Id,
            };

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    game.Cells[r, c] = 0;
                }
            }

            int id = gameRepository.Save(game);

            var read1 = gameRepository.GetByGameId(id);

            read1.Cells[2, 2] = read1.Cells[2, 2] == 0 ? 1 : 0;

            gameRepository.Save(read1);
            var read2 = gameRepository.GetByGameId(id);

            Assert.AreEqual(read1.Cells[2, 2], read2.Cells[2, 2]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var gameRepository = new GameRepositoryEF();

            var user = userRepository.GetAll().FirstOrDefault();
            Assert.IsNotNull(user);

            var game = new GameDto
            {
                UserId = user.Id,
            };

            int id = gameRepository.Save(game);

            Assert.IsNotNull(gameRepository.GetByGameId(id));

            gameRepository.Remove(id);

            Assert.IsNull(gameRepository.GetByGameId(id));
        }
    }
}
