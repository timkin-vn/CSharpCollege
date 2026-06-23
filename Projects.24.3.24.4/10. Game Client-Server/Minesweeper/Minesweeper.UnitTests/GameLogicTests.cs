using Minesweeper.Common;
using Minesweeper.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.UnitTests
{
    [TestClass]
    public class GameLogicTests
    {
        [TestMethod]
        public void Initialize_CreatesEmptyField()
        {
            var game = GameLogic.Initialize();
            int n = Constants.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                {
                    Assert.IsNotNull(game.Field[r, c]);
                    Assert.IsFalse(game.Field[r, c].IsRevealed);
                    Assert.IsFalse(game.Field[r, c].IsMine);
                }
            Assert.IsFalse(game.MinesPlaced);
            Assert.IsFalse(game.IsLost);
        }

        [TestMethod]
        public void Reveal_FirstClickIsSafeAndPlacesMines()
        {
            var game = GameLogic.Initialize();
            bool changed = GameLogic.Reveal(game, 0, 0);

            Assert.IsTrue(changed);
            Assert.IsTrue(game.MinesPlaced);
            Assert.IsFalse(game.IsLost);
            Assert.IsTrue(game.Field[0, 0].IsRevealed);
            Assert.IsFalse(game.Field[0, 0].IsMine);
        }

        [TestMethod]
        public void ToggleFlag_TogglesFlagState()
        {
            var game = GameLogic.Initialize();

            Assert.IsTrue(GameLogic.ToggleFlag(game, 2, 3));
            Assert.IsTrue(game.Field[2, 3].IsFlagged);

            Assert.IsTrue(GameLogic.ToggleFlag(game, 2, 3));
            Assert.IsFalse(game.Field[2, 3].IsFlagged);
        }

        [TestMethod]
        public void Reveal_MineCausesLoss()
        {
            var game = new GameModel();
            game.MinesPlaced = true;
            game.Field[0, 0].IsMine = true;

            bool changed = GameLogic.Reveal(game, 0, 0);

            Assert.IsTrue(changed);
            Assert.IsTrue(game.IsLost);
            Assert.IsTrue(GameLogic.IsGameOver(game));
            Assert.IsFalse(GameLogic.IsWon(game));
        }

        [TestMethod]
        public void IsWon_TrueWhenAllSafeCellsRevealed()
        {
            var game = new GameModel();
            game.MinesPlaced = true;
            int n = Constants.Size;

            int placed = 0;
            for (int r = 0; r < n && placed < Constants.MineCount; r++)
                for (int c = 0; c < n && placed < Constants.MineCount; c++)
                {
                    game.Field[r, c].IsMine = true;
                    placed++;
                }

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (!game.Field[r, c].IsMine)
                        game.Field[r, c].IsRevealed = true;

            Assert.IsTrue(GameLogic.IsWon(game));
        }

        [TestMethod]
        public void Reveal_FlaggedCellIsIgnored()
        {
            var game = GameLogic.Initialize();
            GameLogic.ToggleFlag(game, 4, 4);

            bool changed = GameLogic.Reveal(game, 4, 4);

            Assert.IsFalse(changed);
            Assert.IsFalse(game.Field[4, 4].IsRevealed);
        }
    }
}
