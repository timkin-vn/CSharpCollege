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
                MoveCount = 0,
                GameBegin = DateTime.Now,
            };

            var gameCells = new[,]
            {
                { "Ford", "Ford", "Geely", "Geely", },
                { "Lada", "Lada", "BMW", "BMW", },
                { "Tesla", "Tesla","DS" , "DS", },
                { "Jaguar", "Jaguar", "Skoda", "Skoda" },
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
            Assert.IsTrue((gameDto.GameBegin - readGameDto.GameBegin.ToLocalTime()).TotalSeconds < 1);
        }

        [TestMethod]
        public void GetGamesByUserTest()
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
            string FistTextButon="",SecondTextButton="";
            int gameCount = 0;
            int[] secodn = { 0, 0 }, fist = { 0, 0 };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (gameCount==0)
                    {
                        FistTextButon = game.Cells[row, column];
                        fist[0] = row;
                        fist[1] = column;
                      gameCount++;
                        if (FistTextButon == null || SecondTextButton == null)
                        {
                            throw new Exception("Ошибка в БД");
                        }

                        if (FistTextButon == SecondTextButton && (fist != secodn))
                        {
                            game.Cells[fist[0], fist[1]] = "";
                            game.Cells[secodn[0], secodn[1]] = "";

                        }
                    }
                    else
                    {
                        SecondTextButton = game.Cells[row, column];
                        secodn[0] = row;
                        secodn[1] = column;
                        gameCount =0;
                        if (FistTextButon == null || SecondTextButton == null)
                        {
                            throw new Exception("Ошибка в БД");
                        }

                        if (FistTextButon == SecondTextButton && (fist != secodn))
                        {
                            game.Cells[fist[0], fist[1]] = "";
                            game.Cells[secodn[0], secodn[1]] = "";

                        }
                    }
                }
            }

            
            

            gameRepository.Save(game);
            var readGame = gameRepository.GetByGameId(game.Id);

            Assert.AreEqual(game.MoveCount, readGame.MoveCount);
            Assert.AreEqual(game.Cells[fist[0], fist[1]], readGame.Cells[fist[0], fist[1]]);
            Assert.AreEqual(game.Cells[secodn[0], secodn[1]], readGame.Cells[secodn[0], secodn[1]]);
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

            var gameIds = new List<int>();
            foreach (var user in users)
            {
                gameIds.AddRange(gameRepository.GetByUserId(user.Id).Select(g => g.Id));
            }

            Assert.IsFalse(gameIds.Contains(game.Id));
        }
    }
}
