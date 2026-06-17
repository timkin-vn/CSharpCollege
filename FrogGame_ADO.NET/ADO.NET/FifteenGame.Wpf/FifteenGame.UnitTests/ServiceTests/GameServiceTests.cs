using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrogGame.UnitTests.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void InitializeGameTest()
        {
            var gameService = new GameService(new GameRepository());
            var gameModel = new GameModel();

            gameService.Initialize(gameModel);

            Assert.AreEqual(0, gameModel.MoveCount);
            Assert.IsFalse(gameModel.IsGameOver);
            Assert.IsFalse(gameModel.IsWin);

            Assert.AreEqual(0, gameModel.FrogRow);
            Assert.AreEqual(0, gameModel.FrogColumn);
            Assert.AreEqual(7, gameModel.HomeRow);
            Assert.AreEqual(7, gameModel.HomeColumn);

            Assert.AreEqual((int)CellType.Frog, gameModel[0, 0]);
            Assert.AreEqual((int)CellType.Home, gameModel[7, 7]);
        }

        [TestMethod]
        public void RemoveAlgaeTest()
        {
            var gameService = new GameService(new GameRepository());
            var gameModel = new GameModel();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    gameModel[row, col] = (int)CellType.Water;
                }
            }

            gameModel[0, 0] = (int)CellType.Frog;
            gameModel[7, 7] = (int)CellType.Home;
            gameModel[0, 1] = (int)CellType.LilyPad;
            gameModel[1, 0] = (int)CellType.Algae;

            bool result = gameService.RemoveAlgae(gameModel, 1, 0);

            Assert.IsTrue(result, "RemoveAlgae должна вернуть true");
            Assert.AreEqual((int)CellType.LilyPad, gameModel[1, 0],
                           "Водоросли должны превратиться в кувшинку");
            Assert.AreEqual(1, gameModel.MoveCount, "Счетчик ходов должен увеличиться");
        }

        [TestMethod]
        public void SelectAndMoveLilyPadTest()
        {
            var gameService = new GameService(new GameRepository());
            var gameModel = new GameModel();
            gameService.Initialize(gameModel);
            bool lilyPadFound = false;
            int lilyRow = -1, lilyCol = -1;
            int waterRow = -1, waterCol = -1;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var cellType = (CellType)gameModel[row, col];
                    if (!lilyPadFound && cellType == CellType.LilyPad)
                    {
                        lilyPadFound = true;
                        lilyRow = row;
                        lilyCol = col;
                    }
                    else if (cellType == CellType.Water)
                    {
                        waterRow = row;
                        waterCol = col;
                    }
                }
            }

            if (lilyPadFound && waterRow != -1)
            {
                bool selectResult = gameService.SelectLilyPad(gameModel, lilyRow, lilyCol);
                Assert.IsTrue(selectResult);
                Assert.AreEqual(lilyRow, gameModel.SelectedLilyPadRow);
                Assert.AreEqual(lilyCol, gameModel.SelectedLilyPadColumn);
                bool moveResult = gameService.MoveLilyPad(gameModel, waterRow, waterCol);
                Assert.IsTrue(moveResult);
                Assert.AreEqual((int)CellType.LilyPad, gameModel[waterRow, waterCol]);
                Assert.AreEqual((int)CellType.Water, gameModel[lilyRow, lilyCol]);
            }
        }
    }
}