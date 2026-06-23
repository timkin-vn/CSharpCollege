using SeaBattle.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeaBattle.UnitTests
{
    [TestClass]
    public class GameLogicTests
    {
        [TestMethod]
        public void PlaceFleet_PlacesTwentyDeckCells()
        {
            var board = new Board();
            GameLogic.PlaceFleet(board);

            int decks = 0;
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    if (board.Cells[r, c] == CellState.Ship) decks++;

            Assert.AreEqual(20, decks);
            Assert.AreEqual(10, board.Ships.Count);
        }

        [TestMethod]
        public void PlaceFleet_ShipsDoNotTouch()
        {
            var board = new Board();
            GameLogic.PlaceFleet(board);

            var owner = new int[Board.Size, Board.Size];
            for (int i = 0; i < board.Ships.Count; i++)
                foreach (var cell in board.Ships[i].Cells)
                    owner[cell[0], cell[1]] = i + 1;

            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                {
                    if (owner[r, c] == 0) continue;
                    for (int dr = -1; dr <= 1; dr++)
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            int nr = r + dr, nc = c + dc;
                            if (nr < 0 || nr >= Board.Size || nc < 0 || nc >= Board.Size) continue;
                            if (owner[nr, nc] != 0 && owner[nr, nc] != owner[r, c])
                                Assert.Fail("Корабли соприкасаются");
                        }
                }
        }

        [TestMethod]
        public void Shoot_MissThenHitThenSunk()
        {
            var board = new Board();
            var ship = new Ship();
            ship.Cells.Add(new[] { 0, 0 });
            board.Ships.Add(ship);
            board.Cells[0, 0] = CellState.Ship;

            Assert.AreEqual(ShotResult.Miss, board.Shoot(5, 5));
            Assert.AreEqual(ShotResult.Sunk, board.Shoot(0, 0));
            Assert.AreEqual(ShotResult.Invalid, board.Shoot(0, 0));
            Assert.IsTrue(board.AllSunk());
        }

        [TestMethod]
        public void Ai_EventuallySinksWholeFleet()
        {
            var board = new Board();
            GameLogic.PlaceFleet(board);
            var ai = new AiPlayer();

            int shots = 0;
            while (!board.AllSunk() && shots < Board.Size * Board.Size)
            {
                int r, c;
                ai.Fire(board, out r, out c);
                shots++;
            }
            Assert.IsTrue(board.AllSunk());
        }
    }
}
