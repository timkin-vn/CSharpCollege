using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Игра.Common.Definitions;
using Игра.Common.Dtos;
using Игра.DataAccess.EF.Entites;
using Игра.DataAccess.EF.Repositories;

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
                { 1, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 },
            };

            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    gameDto.Cells[r, c] = gameCells[r, c];
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
            var users = userRepository.GetAll().ToList();

            if (!users.Any())
            {
                var testUser = userRepository.Create("TestUserForGameTest");
                users = new List<UserDto> { testUser };
            }

            var gameRepository = new GameRepositoryEF();

            int gameCount = 0;
            foreach (var user in users)
            {
                var games = gameRepository.GetByUserId(user.Id).ToList();

                if (!games.Any())
                {
                    var gameDto = new GameDto
                    {
                        UserId = user.Id,
                        MoveCount = 0
                    };
                    gameRepository.Save(gameDto);
                    games = gameRepository.GetByUserId(user.Id).ToList();
                }

                gameCount += games.Count;
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
            int r = 0, c = 0;
            for (r = 0; r < Constants.Size; r++)
            {
                for (c = 0; c < Constants.Size; c++)
                {
                    if (game.Cells[r, c] == 1)
                    {
                        game.Cells[r, c] = 0;
                        int nr = (r + 1) % Constants.Size;
                        game.Cells[nr, c] = 1;
                        goto changed;
                    }
                }
            }
            changed:
            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
        }

        [TestMethod]
        public void RemoveGameTest()
        {
            var userRepository = new UserRepositoryEF();
            var users = userRepository.GetAll().ToList();

            if (!users.Any())
            {
                var testUser = userRepository.Create("TestUserForGameTest");
                users = new List<UserDto> { testUser };
            }

            var selectedUser = users.First();
            var gameRepository = new GameRepositoryEF();
            var games = gameRepository.GetByUserId(selectedUser.Id).ToList();

            if (!games.Any())
            {
                var gameDto = new GameDto
                {
                    UserId = selectedUser.Id,
                    MoveCount = 0
                };
                gameRepository.Save(gameDto);
                games = gameRepository.GetByUserId(selectedUser.Id).ToList();
            }

            var game = games.First();
            gameRepository.Remove(game.Id);
            var readGame = gameRepository.GetByGameId(game.Id);
            Assert.IsNull(readGame);
        }
    }
}