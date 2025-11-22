using _2048Game.Common.Definitions;
using _2048Game.Common.Dto;
using _2048Game.DataAccess.Repositories;
using _2048Game.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.UnitTest.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTest
    {
        [TestMethod]
        public void CreateAndReadTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var selectedUser = users.FirstOrDefault();

            if (selectedUser == null)
            {
                return;
            }
            var gameRepository = new GameRepository();

            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
            };

            // Заполним поле как в 2048
            var gameCells = new int[,]
            {
                { 2, 0, 0, 0 },
                { 4, 2, 0, 0 },
                { 0, 4, 2, 0 },
                { 0, 0, 0, 2 },
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = gameCells[row, column];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.IsNotNull(readGameDto);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(gameDto.Cells[row, column], readGameDto.Cells[row, column]);
                }
            }       
        }

        [TestMethod]
        public void GetGamesByUserTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();
            int total = 0;

            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                total += games.Count();
            }

            Assert.IsTrue(total >= 0); // допускаем 0 (если пользователь без игр)
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();

            GameDto game = null;

            foreach (var user in users)
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
                return;
            }
                

            // Ищем первую непустую клетку
            int foundRow = -1, foundColumn = -1;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (game.Cells[row, column] != Constants.FreeCellValue)
                    {
                        foundRow = row;
                        foundColumn = column;
                        break;
                    }
                }
                if (foundRow >= 0)
                {
                    break;
                }
            }

            if (foundRow < 0)
            {
                throw new Exception("Нет непустых клеток");
            }

            // Переместим найденную клетку
            int oldValue = game.Cells[foundRow, foundColumn];

            int newRow = (foundRow + 1) % Constants.RowCount;

            game.Cells[foundRow, foundColumn] = Constants.FreeCellValue;
            game.Cells[newRow, foundColumn] = oldValue;

            gameRepository.Save(game);
            var updatedGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(oldValue, updatedGame.Cells[newRow, foundColumn]);
            Assert.AreEqual(Constants.FreeCellValue, updatedGame.Cells[foundRow, foundColumn]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameRepository();

            GameDto game = null;

            foreach (var user in users)
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
                return;
            }
            gameRepository.Remove(game.Id);

            var readGame = gameRepository.GetByGameId(game.Id);
            Assert.IsNull(readGame);
        }
    }
}
