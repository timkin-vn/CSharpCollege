using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
                Assert.Inconclusive("Нет пользователей в БД для проведения теста.");
                return;
            }

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = user.Id,
                Money = Constants.InitialMoney,
                MoveCount = 0,
            };

            // Заполняем тестовую матрицу 5х5 новыми данными симулятора
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.PeopleCount[row, column] = (row + 1) * (column + 1) * 5; // Просто тестовые числа
                    gameDto.HasShop[row, column] = (row == 2 && column == 2);        // Поставим один ларек в центр
                    gameDto.IsVeggie[row, column] = (row % 2 == 0);                  // ЗОЖники через ряд
                    gameDto.IsRevealed[row, column] = false;                         // Всё покрыто туманом
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            // Проверяем базовые поля
            Assert.IsNotNull(readGameDto);
            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
            Assert.AreEqual(gameDto.Money, readGameDto.Money);

            // Проверяем, что матрицы сохранились и прочитались корректно
            Assert.AreEqual(gameDto.PeopleCount[2, 2], readGameDto.PeopleCount[2, 2]);
            Assert.AreEqual(gameDto.HasShop[2, 2], readGameDto.HasShop[2, 2]);
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

            Assert.IsTrue(gameCount > 0, "В базе должна быть хотя бы одна игра для прохождения теста.");
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
                Assert.Inconclusive("Нет игр в БД для теста обновления.");
                return;
            }

            game.MoveCount++;
            game.Money -= Constants.ShopCost; // Симулируем покупку ларька

            // Ставим новый ларёк в правый нижний угол и открываем клетку
            int targetRow = Constants.RowCount - 1;
            int targetCol = Constants.ColumnCount - 1;
            game.HasShop[targetRow, targetCol] = true;
            game.IsRevealed[targetRow, targetCol] = true;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            // Проверяем что изменения успешно перезаписались в базу
            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Money, readGame.Money);
            Assert.IsTrue(readGame.HasShop[targetRow, targetCol]);
            Assert.IsTrue(readGame.IsRevealed[targetRow, targetCol]);
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
                throw new AssertFailedException("Нет игр для удаления");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame, "Игра должна быть полностью удалена из базы данных.");
        }
    }
}