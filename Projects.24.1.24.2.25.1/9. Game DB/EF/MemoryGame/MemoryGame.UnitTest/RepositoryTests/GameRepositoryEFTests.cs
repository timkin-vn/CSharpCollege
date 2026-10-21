using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Definitions;
using MemoryGame.Common.Dtos;
using MemoryGame.DataAccess.EF.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.UnitTest.RepositoryTests
{
    [TestClass]
    public class GameRepositoryEFTests
    {
        private static readonly int[,] PresetGameCells =
        {
            { 1, 2, 3, 4, },
            { 5, 6, 7, 8, },
            { 1, 2, 3, 4, },
            { 5, 6, 7, 8, },
        };

        [TestMethod]
        public void CreateAndReadGameTest()
        {
            var gameRepository = new GameRepositoryEF();
            var gameDto = CreatePresetGame(GetTestUserId());

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.IsNotNull(readGameDto);
            Assert.AreEqual(gameDto.MoveCount, readGameDto.MoveCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(PresetGameCells[row, column], readGameDto.Cells[row, column]);
                    Assert.AreEqual((int)CardState.Hidden, readGameDto.States[row, column]);
                }
            }
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userId = GetTestUserId();
            var gameRepository = new GameRepositoryEF();
            var gameId = gameRepository.Save(CreatePresetGame(userId));

            var games = gameRepository.GetByUserId(userId);

            Assert.IsTrue(games.Any(g => g.Id == gameId));
            Assert.IsTrue(games.All(g => g.UserId == userId));
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var gameRepository = new GameRepositoryEF();
            var gameId = gameRepository.Save(CreatePresetGame(GetTestUserId()));
            var game = gameRepository.GetByGameId(gameId);

            // Пара (0, 0) - (2, 0) найдена, карточка (1, 1) открыта
            game.MoveCount++;
            game.States[0, 0] = (int)CardState.Matched;
            game.States[2, 0] = (int)CardState.Matched;
            game.States[1, 1] = (int)CardState.Opened;

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Assert.AreEqual(game.Cells[row, column], readGame.Cells[row, column]);
                    Assert.AreEqual(game.States[row, column], readGame.States[row, column]);
                }
            }
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var gameRepository = new GameRepositoryEF();
            var gameId = gameRepository.Save(CreatePresetGame(GetTestUserId()));

            gameRepository.Remove(gameId);
            var readGame = gameRepository.GetByGameId(gameId);

            Assert.IsNull(readGame);
        }

        private static int GetTestUserId()
        {
            var userRepository = new UserRepositoryEF();
            var user = userRepository.GetAll().FirstOrDefault() ?? userRepository.Create("TestUser0");
            return user.Id;
        }

        private static GameDto CreatePresetGame(int userId)
        {
            var gameDto = new GameDto
            {
                UserId = userId,
                MoveCount = 0,
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = PresetGameCells[row, column];
                    gameDto.States[row, column] = (int)CardState.Hidden;
                }
            }

            return gameDto;
        }
    }
}
