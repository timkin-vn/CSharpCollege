using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.DataAccess.Repoistories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.UnitTest.RepositoryTests
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void CreateGameTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var firstUser = users.FirstOrDefault();
            if (firstUser == null)
            {
                return;
            }

            var gameRepository = new GameRepository();
            var gameStart = DateTime.Now;
            var gameDto = new GameDto
            {
                UserId = firstUser.Id,
                MoveCount = 0,
                GameStart = gameStart,
            };

            var cellValues = new[,]
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
                    gameDto.Cells[row, column] = cellValues[row, column];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.IsTrue(Math.Abs((gameDto.GameStart - readGameDto.GameStart.ToLocalTime()).TotalSeconds) < 1);

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(gameDto.Cells[row, column], readGameDto.Cells[row, column]);
                }
            }
        }

        [TestMethod]
        public void GetByUserTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();
            var gameRepository = new GameRepository();
            var gameCount = 0;

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
                throw new Exception("Игр нет");
            }

            int freeCellRow = -1;
            int freeCellColumn = -1;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (game.Cells[row, column] == Constants.FreeCellValue)
                    {
                        freeCellRow = row;
                        freeCellColumn = column;
                    }
                }
            }

            if (freeCellRow < 0 || freeCellColumn < 0)
            {
                throw new Exception("Данные неверны");
            }

            int changedRow = 0;
            if (freeCellRow > 0)
            {
                changedRow = freeCellRow - 1;
            }
            else
            {
                changedRow = freeCellRow + 1;
            }

            game.Cells[freeCellRow, freeCellColumn] = game.Cells[changedRow, freeCellColumn];
            game.Cells[changedRow, freeCellColumn] = Constants.FreeCellValue;
            game.MoveCount++;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Cells[freeCellRow, freeCellColumn], readGame.Cells[freeCellRow, freeCellColumn]);
            Assert.AreEqual(game.Cells[changedRow, freeCellColumn], readGame.Cells[changedRow, freeCellColumn]);
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
                throw new Exception("Игр нет");
            }

            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.IsNull(readGame);
        }
    }
}
