using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwentyFortyEight.Business.Services;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Dtos;
using System.Collections.Generic;

namespace TwentyFortyEight.UnitTest.ServiceTests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService _service;

        [TestInitialize]
        public void Init()
        {
            _service = new GameService(new StubGameRepository());
        }

        [TestMethod]
        public void GetByUserId_CreatesNewGame()
        {
            var model = _service.GetByUserId(1);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.UserId);
            Assert.AreEqual(0, model.Score);
        }

        [TestMethod]
        public void GetByUserId_ReturnsSameGame_OnSecondCall()
        {
            var m1 = _service.GetByUserId(2);
            var m2 = _service.GetByUserId(2);
            Assert.AreSame(m1, m2);
        }

        [TestMethod]
        public void NewGame_HasExactlyTwoTiles()
        {
            var model = _service.GetByUserId(10);
            int count = 0;
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (model[r, c] != 0) count++;
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void MakeMove_Left_DoesNotReturnNull()
        {
            var model = _service.GetByUserId(3);
            var after = _service.MakeMove(model.Id, MoveDirection.Left);
            Assert.IsNotNull(after);
        }

        [TestMethod]
        public void IsGameOver_ReturnsFalse_ForNewGame()
        {
            var model = _service.GetByUserId(4);
            Assert.IsFalse(_service.IsGameOver(model.Id));
        }

        [TestMethod]
        public void ResetGame_CreatesNewGameState()
        {
            var m1 = _service.GetByUserId(5);
            int oldScore = m1.Score;
            m1.Score = 9999; // mutate manually
            _service.ResetGame(5);
            var m2 = _service.GetByUserId(5);
            Assert.AreEqual(0, m2.Score, "Score should reset to 0");
        }

        // ── Stub ─────────────────────────────────────────────────────────
        private class StubGameRepository : IGameRepository
        {
            public GameDto GetByGameId(int gameId) => null;
            public IEnumerable<GameDto> GetByUserId(int userId) => new List<GameDto>();
            public void Remove(int gameId) { }
            public int Save(GameDto gameDto) => 1;
        }
    }
}
