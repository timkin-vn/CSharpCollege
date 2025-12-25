using Game2048.Common.Models;
using Game2048.Common.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game2048.UnitTests.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService _gameService;

        [TestInitialize]
        public void Setup()
        {
            _gameService = new GameService();
        }

        [TestMethod]
        public void InitializeGame_ShouldCreateGameWithTwoTiles()
        {
            // Act
            var game = _gameService.InitializeGame();

            // Assert
            Assert.IsNotNull(game);
            int nonZeroCount = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (game.Board[row, col] != 0)
                        nonZeroCount++;
                }
            }
            Assert.AreEqual(2, nonZeroCount);
            Assert.AreEqual(0, game.Score);
            Assert.IsFalse(game.IsGameOver);
            Assert.IsFalse(game.IsWon);
        }

        [TestMethod]
        public void MoveLeft_ShouldMergeTiles()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            game.Board[0, 0] = 2;
            game.Board[0, 1] = 2;
            game.Board[0, 2] = 4;
            game.Board[0, 3] = 0;

            // Act
            var result = _gameService.Move(game, MoveDirection.Left);

            // Assert
            Assert.AreEqual(4, result.Board[0, 0]); // Merged 2+2
            Assert.AreEqual(4, result.Board[0, 1]); // Moved 4
            Assert.AreEqual(0, result.Board[0, 2]);
            Assert.AreEqual(0, result.Board[0, 3]);
            Assert.IsTrue(result.Score > game.Score); // Score should increase
        }

        [TestMethod]
        public void MoveRight_ShouldMergeTiles()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            game.Board[0, 2] = 2;
            game.Board[0, 3] = 2;

            // Act
            var result = _gameService.Move(game, MoveDirection.Right);

            // Assert
            Assert.AreEqual(0, result.Board[0, 0]);
            Assert.AreEqual(0, result.Board[0, 1]);
            Assert.AreEqual(0, result.Board[0, 2]);
            Assert.AreEqual(4, result.Board[0, 3]); // Merged tiles
        }

        [TestMethod]
        public void MoveUp_ShouldMergeTiles()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            game.Board[0, 0] = 2;
            game.Board[1, 0] = 2;

            // Act
            var result = _gameService.Move(game, MoveDirection.Up);

            // Assert
            Assert.AreEqual(4, result.Board[0, 0]); // Merged tiles
            Assert.AreEqual(0, result.Board[1, 0]);
            Assert.AreEqual(0, result.Board[2, 0]);
            Assert.AreEqual(0, result.Board[3, 0]);
        }

        [TestMethod]
        public void MoveDown_ShouldMergeTiles()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            game.Board[2, 0] = 2;
            game.Board[3, 0] = 2;

            // Act
            var result = _gameService.Move(game, MoveDirection.Down);

            // Assert
            Assert.AreEqual(0, result.Board[0, 0]);
            Assert.AreEqual(0, result.Board[1, 0]);
            Assert.AreEqual(0, result.Board[2, 0]);
            Assert.AreEqual(4, result.Board[3, 0]); // Merged tiles
        }

        [TestMethod]
        public void Move_When2048Reached_ShouldSetIsWon()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            game.Board[0, 0] = 1024;
            game.Board[0, 1] = 1024;

            // Act
            var result = _gameService.Move(game, MoveDirection.Left);

            // Assert
            Assert.IsTrue(result.IsWon);
        }

        [TestMethod]
        public void Move_WhenNoValidMoves_ShouldSetIsGameOver()
        {
            // Arrange - Create a board with no valid moves
            var game = _gameService.InitializeGame();
            game.Board[0, 0] = 2; game.Board[0, 1] = 4; game.Board[0, 2] = 8; game.Board[0, 3] = 16;
            game.Board[1, 0] = 4; game.Board[1, 1] = 8; game.Board[1, 2] = 16; game.Board[1, 3] = 32;
            game.Board[2, 0] = 8; game.Board[2, 1] = 16; game.Board[2, 2] = 32; game.Board[2, 3] = 64;
            game.Board[3, 0] = 16; game.Board[3, 1] = 32; game.Board[3, 2] = 64; game.Board[3, 3] = 128;

            // Act
            var result = _gameService.Move(game, MoveDirection.Left);

            // Assert
            Assert.IsTrue(result.IsGameOver);
        }

        [TestMethod]
        public void Move_WhenNoChange_ShouldNotAddNewTile()
        {
            // Arrange
            var game = _gameService.InitializeGame();
            // Fill board completely
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    game.Board[row, col] = 2;
                }
            }

            int initialNonZeroCount = 16;

            // Act
            var result = _gameService.Move(game, MoveDirection.Left);

            // Assert
            int finalNonZeroCount = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (result.Board[row, col] != 0)
                        finalNonZeroCount++;
                }
            }
            Assert.AreEqual(initialNonZeroCount, finalNonZeroCount);
        }
    }
}
