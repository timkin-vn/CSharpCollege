using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTests.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTests
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            var user = allUsers.FirstOrDefault();

            if (user == null)
            {
                Assert.Inconclusive("В базе данных нет пользователей для проведения теста.");
                return;
            }

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = user.Id,
                MoveCount = 0,
                Money = Constants.InitialMoney
            };

            // Заполняем тестовую карту шаурмичных
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.PeopleCount[row, column] = (row + 1) * (column + 1);
                    gameDto.HasShop[row, column] = false;
                    gameDto.IsVeggie[row, column] = (row == 0 && column == 0); // Сделаем одну клетку ЗОЖной
                    gameDto.IsRevealed[row, column] = false;
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            // Проверяем базовые поля
            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.Money, readGameDto.Money);

            // Проверяем сохранение матриц на примере одной клетки
            Assert.AreEqual(gameDto.PeopleCount[0, 0], readGameDto.PeopleCount[0, 0]);
            Assert.AreEqual(gameDto.IsVeggie[0, 0], readGameDto.IsVeggie[0, 0]);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            var gameRepository = new GameRepositoryEF();

            int gameCount = 0;
            foreach (var user in allUsers)
            {
                var games = gameRepository.GetByUserId(user.Id);
                gameCount += games.Count();
            }

            Assert.IsTrue(gameCount > 0, "В базе данных должны быть игры для прохождения теста.");
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            var gameRepository = new GameRepositoryEF();
            GameDto game = null;

            foreach (var user in allUsers)
            {
                var games = gameRepository.GetByUserId(user.Id);
                if (games.Any())
                {
                    game = games.First();
                    break;
                }
            }

            if (game == null)
            {
                Assert.Inconclusive("В базе данных нет существующих игр для обновления.");
                return;
            }

            // Имитируем ход: увеличиваем ходы, меняем деньги и ставим ларёк в [0,0]
            game.MoveCount++;
            game.Money -= Constants.ShopCost;
            game.HasShop[0, 0] = true;
            game.IsRevealed[0, 0] = true;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            // Сверяем обновлённые данные
            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Money, readGame.Money);
            Assert.AreEqual(true, readGame.HasShop[0, 0]);
            Assert.AreEqual(true, readGame.IsRevealed[0, 0]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var allUsers = userRepository.GetAll();
            var gameRepository = new GameRepositoryEF();
            GameDto game = null;

            foreach (var user in allUsers)
            {
                var games = gameRepository.GetByUserId(user.Id);
                if (games.Any())
                {
                    game = games.First();
                    break;
                }
            }

            if (game == null)
            {
                throw new AssertFailedException("Нет игр для тестирования удаления.");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}