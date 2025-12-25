using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
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
    public class GameRepositoryTests
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
                MatchesCount = 0,
                IsFinished = false
            };

            var gameCells = new int[GameModel.RowCount, GameModel.ColumnCount];

            var rnd = new Random();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    gameCells[row, column] = rnd.Next(1, 6);
                }
            }

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    gameDto.Cells[row, column] = gameCells[row, column];
                }
            }

            var gameId = gameRepository.Save(gameDto);
            var readGameDto = gameRepository.GetByGameId(gameId);

            Assert.AreEqual(gameDto.MatchesCount, readGameDto.MatchesCount);
            Assert.AreEqual(gameDto.UserId, readGameDto.UserId);
        }

        [TestMethod]
        public void GetGamesByUserIdTest()
        {
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();

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
            var userRepository = new UserRepository();
            var users = userRepository.GetAll();

            var gameRepository = new GameRepository();
            GameDto gameDto = null;

            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id);
                if (games.Any())
                {
                    gameDto = games.First();
                    break;
                }
            }

            if (gameDto == null)
                return;

            var service = new GameService(gameRepository);
            var model = service.GetByGameId(gameDto.Id);

            int r1 = 0, c1 = 0, r2 = 0, c2 = 1;
            service.Swap(model, r1, c1, r2, c2);
            service.AddMatches(model, 3);

            gameRepository.Save(service.ToDto(model));

            var readDto = gameRepository.GetByGameId(model.Id);
            var readModel = service.GetByGameId(model.Id);

            Assert.AreEqual(model.MatchesCount, readModel.MatchesCount);
            Assert.AreEqual(model[r1, c1], readModel[r1, c1]);
            Assert.AreEqual(model[r2, c2], readModel[r2, c2]);
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
