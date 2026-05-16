using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pacman.Business.Services;
using Pacman.Common;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Models;

namespace Pacman.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IMapRepository> _mapRepositoryMock;
        private GameService _gameService;

        [TestInitialize]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _mapRepositoryMock = new Mock<IMapRepository>();
            _gameService = new GameService(_gameRepositoryMock.Object, _mapRepositoryMock.Object);
        }

        [TestMethod]
        public void CreateNewGame_ShouldInitializeGameWithCorrectValues()
        {
            // Arrange
            var userId = 1;
            var mapDto = CreateTestMap();
            var expectedGameState = CreateInitialGameState();

            _mapRepositoryMock.Setup(m => m.GetDefaultMap()).Returns(mapDto);
            _gameRepositoryMock.Setup(r => r.CreateGame(userId, mapDto.Id)).Returns(expectedGameState);

            // Act
            var result = _gameService.CreateNewGame(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Constants.InitialLives, result.Game.Lives);
            Assert.AreEqual(0, result.Game.Score);
            Assert.AreEqual(GameStatus.InProgress, result.Game.Status);
        }

        [TestMethod]
        public void Move_PacmanCollectsPellet_ShouldIncreaseScore()
        {
            // Arrange
            var gameState = CreateGameStateWithPellet();
            _gameRepositoryMock.Setup(r => r.GetGameState(1)).Returns(gameState);

            // Act
            var result = _gameService.Move(1, Direction.Right);

            // Assert
            Assert.AreEqual(Constants.PelletScore, result.Game.Score);
            _gameRepositoryMock.Verify(r => r.UpdateGameState(It.IsAny<GameStateDto>()), Times.Once);
        }

        [TestMethod]
        public void Move_PacmanCollectsPowerPellet_ShouldFrightenGhosts()
        {
            // Arrange
            var gameState = CreateGameStateWithPowerPellet();
            _gameRepositoryMock.Setup(r => r.GetGameState(1)).Returns(gameState);

            // Act
            var result = _gameService.Move(1, Direction.Right);

            // Assert
            Assert.AreEqual(Constants.PowerPelletScore, result.Game.Score);
            var ghost = result.Actors.First(a => a.ActorType == ActorType.GhostBlinky);
            Assert.AreEqual(Constants.FrightenedDuration, ghost.FrightenedTicksLeft);
        }

        [TestMethod]
        public void Move_AllCollectiblesEaten_ShouldWinGame()
        {
            // Arrange
            var gameState = CreateGameStateWithLastPellet();
            _gameRepositoryMock.Setup(r => r.GetGameState(1)).Returns(gameState);

            // Act
            var result = _gameService.Move(1, Direction.Right);

            // Assert
            Assert.AreEqual(GameStatus.Won, result.Game.Status);
        }

        [TestMethod]
        public void Move_CollisionWithNormalGhost_ShouldLoseLife()
        {
            // Arrange
            var gameState = CreateGameStateWithGhostCollision();
            _gameRepositoryMock.Setup(r => r.GetGameState(1)).Returns(gameState);

            // Act
            var result = _gameService.Move(1, Direction.Right);

            // Assert
            Assert.AreEqual(Constants.InitialLives - 1, result.Game.Lives);
        }

        [TestMethod]
        public void Move_IntoWall_ShouldNotMove()
        {
            // Arrange
            var gameState = CreateGameStateNearWall();
            var originalRow = gameState.Actors.First(a => a.ActorType == ActorType.Pacman).Row;
            var originalCol = gameState.Actors.First(a => a.ActorType == ActorType.Pacman).Col;

            _gameRepositoryMock.Setup(r => r.GetGameState(1)).Returns(gameState);

            // Act
            var result = _gameService.Move(1, Direction.Up);

            // Assert
            var pacman = result.Actors.First(a => a.ActorType == ActorType.Pacman);
            Assert.AreEqual(originalRow, pacman.Row);
            Assert.AreEqual(originalCol, pacman.Col);
        }

        // Helper methods
        private MapDto CreateTestMap()
        {
            var cells = new System.Collections.Generic.List<MapCellDto>();
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    var cellType = (r == 0 || r == 9 || c == 0 || c == 9) ? CellType.Wall : CellType.Empty;
                    cells.Add(new MapCellDto { Row = r, Col = c, CellType = cellType });
                }
            }

            return new MapDto
            {
                Id = 1,
                Name = "TestMap",
                RowCount = 10,
                ColCount = 10,
                Cells = cells
            };
        }

        private GameStateDto CreateInitialGameState()
        {
            return new GameStateDto
            {
                Game = new GameDto
                {
                    Id = 1,
                    UserId = 1,
                    MapId = 1,
                    Status = GameStatus.InProgress,
                    Score = 0,
                    Lives = Constants.InitialLives
                },
                Map = CreateTestMap(),
                Actors = new System.Collections.Generic.List<GameActorDto>
                {
                    new GameActorDto { ActorType = ActorType.Pacman, Row = 5, Col = 5, Direction = Direction.None }
                },
                CollectibleStates = new System.Collections.Generic.List<GameCollectibleStateDto>()
            };
        }

        private GameStateDto CreateGameStateWithPellet()
        {
            var state = CreateInitialGameState();
            state.Actors.First().Col = 4;
            state.CollectibleStates.Add(new GameCollectibleStateDto
            {
                Row = 4,
                Col = 5,
                CollectibleType = CellType.Pellet,
                IsEaten = false
            });
            return state;
        }

        private GameStateDto CreateGameStateWithPowerPellet()
        {
            var state = CreateInitialGameState();
            state.Actors.First().Col = 4;
            state.Actors.Add(new GameActorDto
            {
                ActorType = ActorType.GhostBlinky,
                Row = 1,
                Col = 1,
                Direction = Direction.Down,
                FrightenedTicksLeft = 0
            });
            state.CollectibleStates.Add(new GameCollectibleStateDto
            {
                Row = 4,
                Col = 5,
                CollectibleType = CellType.PowerPellet,
                IsEaten = false
            });
            return state;
        }

        private GameStateDto CreateGameStateWithLastPellet()
        {
            var state = CreateInitialGameState();
            state.Actors.First().Col = 4;
            state.CollectibleStates.Add(new GameCollectibleStateDto
            {
                Row = 4,
                Col = 5,
                CollectibleType = CellType.Pellet,
                IsEaten = false
            });
            return state;
        }

        private GameStateDto CreateGameStateWithGhostCollision()
        {
            var state = CreateInitialGameState();
            state.Actors.First().Row = 4;
            state.Actors.First().Col = 4;
            state.Actors.Add(new GameActorDto
            {
                ActorType = ActorType.GhostBlinky,
                Row = 4,
                Col = 5,
                Direction = Direction.Left,
                FrightenedTicksLeft = 0
            });
            return state;
        }

        private GameStateDto CreateGameStateNearWall()
        {
            var state = CreateInitialGameState();
            state.Actors.First().Row = 1;
            state.Actors.First().Col = 1;
            return state;
        }
    }
}