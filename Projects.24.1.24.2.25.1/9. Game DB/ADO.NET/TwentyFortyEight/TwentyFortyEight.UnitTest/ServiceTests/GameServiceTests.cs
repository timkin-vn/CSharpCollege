using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwentyFortyEight.Business.Services;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace TwentyFortyEight.UnitTest.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService _gameProcessingService;

        [TestInitialize]
        public void SetupTestEnvironment()
        {
            _gameProcessingService = new GameService(new MockGameDataRepository());
        }

        [TestMethod]
        public void VerifyingGameCreation_WhenUserIdProvided()
        {
            var resultModel = _gameProcessingService.GetByUserId(1);

            Assert.IsNotNull(resultModel);
            Assert.AreEqual(0, resultModel.Score);
            Assert.AreEqual(1, resultModel.UserId);
        }

        [TestMethod]
        public void VerifyingGamePersistence_OnRepeatedRequest()
        {
            var firstSession = _gameProcessingService.GetByUserId(2);
            var secondSession = _gameProcessingService.GetByUserId(2);

            Assert.AreSame(firstSession, secondSession);
        }

        [TestMethod]
        public void NewGameGrid_ShouldContainExactlyTwoActiveTiles()
        {
            var activeGame = _gameProcessingService.GetByUserId(10);
            var activeTilesCount = 0;

            var indices = Enumerable.Range(0, 4);
            foreach (var r in indices)
            {
                foreach (var c in indices)
                {
                    if (activeGame[r, c] != 0)
                    {
                        activeTilesCount++;
                    }
                }
            }

            Assert.AreEqual(2, activeTilesCount);
        }

        [TestMethod]
        public void Shifts_LeftDirection_ShouldReturnValidState()
        {
            var activeGame = _gameProcessingService.GetByUserId(3);
            var modifiedGame = _gameProcessingService.MakeMove(activeGame.Id, MoveDirection.Left);

            Assert.IsNotNull(modifiedGame);
        }

        [TestMethod]
        public void FreshGameSession_ShouldNotBeMarkedAsFinished()
        {
            var activeGame = _gameProcessingService.GetByUserId(4);

            Assert.IsFalse(_gameProcessingService.IsGameOver(activeGame.Id));
        }

        [TestMethod]
        public void ReinitializingGame_ShouldClearProgress()
        {
            var session = _gameProcessingService.GetByUserId(5);
            session.Score = 55555;

            _gameProcessingService.ResetGame(5);
            var reloadedSession = _gameProcessingService.GetByUserId(5);

            Assert.AreEqual(0, reloadedSession.Score);
        }

        private class MockGameDataRepository : IGameRepository
        {
            public GameDto GetByGameId(int gameId) => null;
            public IEnumerable<GameDto> GetByUserId(int userId) => new List<GameDto>();
            public void Remove(int gameId) { }
            public int Save(GameDto gameDto) => 1;
        }
    }
}