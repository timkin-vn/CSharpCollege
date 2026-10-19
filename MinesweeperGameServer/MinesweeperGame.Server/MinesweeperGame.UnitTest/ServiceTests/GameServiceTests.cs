using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperGame.Business.Services;
using MinesweeperGame.Common.BusinessModels;
using MinesweeperGame.Common.Contracts;
using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.UnitTest.ServiceTests
{
    public class FakeRepository : IGameRepository
    {
        private Dictionary<Guid, GameBoard> games = new Dictionary<Guid, GameBoard>();

        private GameBoard Copy(GameBoard b)
        {
            var copy = new GameBoard
            {
                Id = b.Id,
                Difficulty = b.Difficulty,
                Status = b.Status,
                Rows = b.Rows,
                Columns = b.Columns,
                TotalMines = b.TotalMines,
                FlagsPlaced = b.FlagsPlaced,
                CreatedAt = b.CreatedAt,
                FinishedAt = b.FinishedAt,
                ElapsedTime = b.ElapsedTime
            };
            foreach (var c in b.Cells)
            {
                copy.Cells.Add(new Cell
                {
                    Row = c.Row,
                    Column = c.Column,
                    IsMine = c.IsMine,
                    AdjacentMinesCount = c.AdjacentMinesCount,
                    State = c.State
                });
            }
            return copy;
        }

        public Task<GameBoard> CreateAsync(GameBoard gameBoard)
        {
            games[gameBoard.Id] = Copy(gameBoard);
            return Task.FromResult(Copy(gameBoard));
        }

        public Task<GameBoard> GetByIdAsync(Guid id)
        {
            if (!games.ContainsKey(id)) return Task.FromResult<GameBoard>(null);
            return Task.FromResult(Copy(games[id]));
        }

        public Task UpdateAsync(GameBoard gameBoard)
        {
            games[gameBoard.Id] = Copy(gameBoard);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public GameBoard Get(Guid id)
        {
            return games[id];
        }
    }

    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public async Task CreateGame_Beginner()
        {
            var service = new GameService(new FakeRepository());
            var res = await service.CreateGameAsync(GameDifficulty.Beginner);

            Assert.IsTrue(res.Success);
            Assert.AreEqual(9, res.Data.Game.Rows);
            Assert.AreEqual(9, res.Data.Game.Columns);
            Assert.AreEqual(81, res.Data.Cells.Count);
            Assert.AreEqual(GameStatus.NotStarted, res.Data.Game.Status);
        }

        [TestMethod]
        public async Task FirstClick_IsSafe_AndMinesSaved()
        {
            for (int i = 0; i < 20; i++)
            {
                var repo = new FakeRepository();
                var service = new GameService(repo);
                var res = await service.CreateGameAsync(GameDifficulty.Expert);
                var id = res.Data.Game.Id;

                res = await service.RevealCellAsync(id, 5, 5);
                Assert.IsTrue(res.Success);
                Assert.AreEqual(GameStatus.InProgress, res.Data.Game.Status);

                var saved = repo.Get(id);
                Assert.AreEqual(99, saved.Cells.Count(c => c.IsMine));
                Assert.IsFalse(saved.GetCell(5, 5).IsMine);
                Assert.AreEqual(0, saved.GetCell(5, 5).AdjacentMinesCount);
            }
        }

        [TestMethod]
        public async Task OpenAllSafeCells_Wins()
        {
            var repo = new FakeRepository();
            var service = new GameService(repo);
            var res = await service.CreateGameAsync(GameDifficulty.Beginner);
            var id = res.Data.Game.Id;

            res = await service.RevealCellAsync(id, 0, 0);
            var saved = repo.Get(id);

            foreach (var c in saved.Cells.Where(x => !x.IsMine).ToList())
            {
                var now = repo.Get(id);
                if (now.Status != GameStatus.InProgress) break;
                if (now.GetCell(c.Row, c.Column).State == CellState.Revealed) continue;
                res = await service.RevealCellAsync(id, c.Row, c.Column);
                Assert.IsTrue(res.Success);
            }

            Assert.AreEqual(GameStatus.Won, repo.Get(id).Status);
        }

        [TestMethod]
        public async Task ClickMine_Loses()
        {
            var repo = new FakeRepository();
            var service = new GameService(repo);
            var res = await service.CreateGameAsync(GameDifficulty.Beginner);
            var id = res.Data.Game.Id;

            await service.RevealCellAsync(id, 4, 4);
            var mine = repo.Get(id).Cells.First(c => c.IsMine);

            res = await service.RevealCellAsync(id, mine.Row, mine.Column);
            Assert.IsTrue(res.Success);
            Assert.AreEqual(GameStatus.Lost, res.Data.Game.Status);
            Assert.AreEqual(10, res.Data.Cells.Count(c => c.IsMine));
        }

        [TestMethod]
        public async Task Flag_OnAndOff()
        {
            var repo = new FakeRepository();
            var service = new GameService(repo);
            var res = await service.CreateGameAsync(GameDifficulty.Beginner);
            var id = res.Data.Game.Id;

            res = await service.ToggleFlagAsync(id, 2, 3);
            Assert.AreEqual(1, res.Data.Game.FlagsPlaced);
            Assert.AreEqual(CellState.Flagged, repo.Get(id).GetCell(2, 3).State);

            res = await service.RevealCellAsync(id, 2, 3);
            Assert.IsFalse(res.Success);

            res = await service.ToggleFlagAsync(id, 2, 3);
            Assert.AreEqual(0, res.Data.Game.FlagsPlaced);
            Assert.AreEqual(CellState.Hidden, repo.Get(id).GetCell(2, 3).State);
        }

        [TestMethod]
        public async Task HiddenMines_NotSentToClient()
        {
            var repo = new FakeRepository();
            var service = new GameService(repo);
            var res = await service.CreateGameAsync(GameDifficulty.Beginner);
            res = await service.RevealCellAsync(res.Data.Game.Id, 0, 0);

            if (res.Data.Game.Status == GameStatus.InProgress)
                Assert.AreEqual(0, res.Data.Cells.Count(c => c.IsMine));
        }
    }
}
