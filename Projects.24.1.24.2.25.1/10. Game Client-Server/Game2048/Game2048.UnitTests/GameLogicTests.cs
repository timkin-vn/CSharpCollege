using Game2048.Common;
using Game2048.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTests
{
    [TestClass]
    public class GameLogicTests
    {
        private static int CountNonZero(GameModel g)
        {
            int n = 0;
            foreach (var v in g.Field) if (v != 0) n++;
            return n;
        }

        [TestMethod]
        public void Initialize_PlacesExactlyTwoTiles()
        {
            var game = GameLogic.Initialize();
            Assert.AreEqual(2, CountNonZero(game));
        }

        [TestMethod]
        public void MakeMove_MergesEqualTilesAndAddsScore()
        {
            var game = new GameModel();
            game.Field[0, 0] = 2;
            game.Field[0, 1] = 2;

            bool changed = GameLogic.MakeMove(game, MoveDirection.Left);

            Assert.IsTrue(changed);
            Assert.AreEqual(4, game.Field[0, 0]);
            Assert.AreEqual(4, game.Score);
            Assert.AreEqual(1, game.MoveCount);
        }

        [TestMethod]
        public void IsWon_TrueWhen2048Present()
        {
            var game = new GameModel();
            game.Field[1, 1] = 2048;
            Assert.IsTrue(GameLogic.IsWon(game));
        }

        [TestMethod]
        public void IsGameOver_TrueWhenBoardFullWithoutMoves()
        {
            var game = new GameModel();
            int[,] pattern =
            {
                { 2, 4, 2, 4 },
                { 4, 2, 4, 2 },
                { 2, 4, 2, 4 },
                { 4, 2, 4, 2 }
            };
            for (int r = 0; r < Constants.Size; r++)
                for (int c = 0; c < Constants.Size; c++)
                    game.Field[r, c] = pattern[r, c];

            Assert.IsTrue(GameLogic.IsGameOver(game));
        }

        [TestMethod]
        public void MakeMove_NoChangeReturnsFalse()
        {
            var game = new GameModel();
            game.Field[0, 0] = 2;
            bool changed = GameLogic.MakeMove(game, MoveDirection.Left);
            Assert.IsFalse(changed);
        }
    }
}
