using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.EF.Repositories;
using FifteenGame.DataAccess.Repositories;
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
            var users = userRepository.GetAll();
            var selectedUser = users.FirstOrDefault();

            if (selectedUser == null)
            {
                return;
            }

            var gameRepository = new GameRepositoryEF();
            var gameDto = new GameDto
            {
                UserId = selectedUser.Id,
                MoveCount = 100,
            };

            var gameCells = new[,]
            {
                { 1, 2, 3, 4, },
                { 5, 6, 7, 8, },
                { 9, 10, 11, 12, },
                { 13, 14, 15, Constants.FreeCellValue, },
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

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepositoryEF();

            int gameCount = 0;
            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                gameCount += games.Count();
            }

            Assert.IsTrue(gameCount > 0);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepositoryEF();
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

            game.MoveCount++;
            int freeRow = -1;
            int freeColumn = -1;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (game.Cells[row, column] == Constants.FreeCellValue)
                    {
                        freeRow = row;
                        freeColumn = column;
                    }
                }
            }

            if (freeRow < 0 || freeColumn < 0)
            {
                throw new Exception("Ошибка в БД");
            }

            if (freeRow > 0)
            {
                game.Cells[freeRow, freeColumn] = game.Cells[freeRow - 1, freeColumn];
                game.Cells[freeRow - 1, freeColumn] = Constants.FreeCellValue;
            }
            else
            {
                game.Cells[freeRow, freeColumn] = game.Cells[freeRow + 1, freeColumn];
                game.Cells[freeRow + 1, freeColumn] = Constants.FreeCellValue;
            }

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Cells[freeRow, freeColumn], readGame.Cells[freeRow, freeColumn]);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll();
            var gameRepository = new GameRepositoryEF();
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
                throw new Exception("Игр нет");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
