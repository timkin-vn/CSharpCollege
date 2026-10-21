using MemoryGame.Business.Services;
using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Definitions;
using MemoryGame.Common.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.UnitTest.ServiceTests
{
    /// <summary>
    /// Проверка правил игры. БД не нужна - используется репозиторий в памяти.
    /// </summary>
    [TestClass]
    public class GameServiceTests
    {
        private const int UserId = 1;

        [TestMethod]
        public void NewGameContainsEveryPictureTwiceTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);

            Assert.AreNotEqual(0, game.Id);
            Assert.AreEqual(UserId, game.UserId);
            Assert.AreEqual(0, game.MoveCount);
            Assert.AreEqual(0, game.MatchedPairCount);

            var values = AllCells().Select(c => game[c.Row, c.Column]).ToList();
            for (int value = 1; value <= Constants.PairCount; value++)
            {
                Assert.AreEqual(2, values.Count(v => v == value));
            }

            Assert.IsTrue(AllCells().All(c => game.GetState(c.Row, c.Column) == CardState.Hidden));
            Assert.AreEqual(false, service.IsGameOver(game.Id));
        }

        [TestMethod]
        public void GetByUserIdReturnsUnfinishedGameTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);
            var first = AllCells().First();
            service.MakeMove(game.Id, first.Row, first.Column);

            var sameGame = service.GetByUserId(UserId);

            Assert.AreEqual(game.Id, sameGame.Id);
            Assert.AreEqual(CardState.Opened, sameGame.GetState(first.Row, first.Column));
        }

        [TestMethod]
        public void MatchingPairStaysOpenTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);
            var (first, second) = FindCells(game, sameValue: true);

            service.MakeMove(game.Id, first.Row, first.Column);
            game = service.MakeMove(game.Id, second.Row, second.Column);

            Assert.AreEqual(CardState.Matched, game.GetState(first.Row, first.Column));
            Assert.AreEqual(CardState.Matched, game.GetState(second.Row, second.Column));
            Assert.AreEqual(1, game.MoveCount);
            Assert.AreEqual(1, game.MatchedPairCount);

            // Щелчок по найденной паре ничего не меняет
            game = service.MakeMove(game.Id, first.Row, first.Column);
            Assert.AreEqual(CardState.Matched, game.GetState(first.Row, first.Column));
            Assert.AreEqual(1, game.MoveCount);
        }

        [TestMethod]
        public void DifferentCardsAreClosedOnNextMoveTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);
            var (first, second) = FindCells(game, sameValue: false);

            service.MakeMove(game.Id, first.Row, first.Column);
            game = service.MakeMove(game.Id, second.Row, second.Column);

            Assert.AreEqual(CardState.Opened, game.GetState(first.Row, first.Column));
            Assert.AreEqual(CardState.Opened, game.GetState(second.Row, second.Column));
            Assert.AreEqual(1, game.MoveCount);
            Assert.AreEqual(0, game.MatchedPairCount);

            var third = AllCells().First(c => c != first && c != second);
            game = service.MakeMove(game.Id, third.Row, third.Column);

            Assert.AreEqual(CardState.Hidden, game.GetState(first.Row, first.Column));
            Assert.AreEqual(CardState.Hidden, game.GetState(second.Row, second.Column));
            Assert.AreEqual(CardState.Opened, game.GetState(third.Row, third.Column));
            Assert.AreEqual(1, game.MoveCount);
        }

        [TestMethod]
        public void ClickOnOpenedCardIsIgnoredTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);
            var first = AllCells().First();

            service.MakeMove(game.Id, first.Row, first.Column);
            game = service.MakeMove(game.Id, first.Row, first.Column);

            Assert.AreEqual(CardState.Opened, game.GetState(first.Row, first.Column));
            Assert.AreEqual(1, AllCells().Count(c => game.GetState(c.Row, c.Column) == CardState.Opened));
            Assert.AreEqual(0, game.MoveCount);
        }

        [TestMethod]
        public void GameIsOverWhenAllPairsFoundTest()
        {
            var service = new GameService(new InMemoryGameRepository());
            var game = service.GetByUserId(UserId);

            for (int value = 1; value <= Constants.PairCount; value++)
            {
                Assert.AreEqual(false, service.IsGameOver(game.Id));

                var pair = AllCells().Where(c => game[c.Row, c.Column] == value).ToList();
                service.MakeMove(game.Id, pair[0].Row, pair[0].Column);
                game = service.MakeMove(game.Id, pair[1].Row, pair[1].Column);
            }

            Assert.AreEqual(true, service.IsGameOver(game.Id));
            Assert.AreEqual(Constants.PairCount, game.MoveCount);
            Assert.AreEqual(Constants.PairCount, game.MatchedPairCount);

            // Завершенная игра не продолжается - пользователь получает новую
            var newGame = service.GetByUserId(UserId);
            Assert.AreNotEqual(game.Id, newGame.Id);
            Assert.AreEqual(0, newGame.MoveCount);

            service.RemoveGame(game.Id);
            Assert.IsNull(service.GetByGameId(game.Id));
            Assert.IsNull(service.IsGameOver(game.Id));
        }

        private static IEnumerable<(int Row, int Column)> AllCells()
        {
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    yield return (row, column);
                }
            }
        }

        private static ((int Row, int Column) First, (int Row, int Column) Second) FindCells(GameModel game, bool sameValue)
        {
            var first = AllCells().First();
            var second = AllCells().First(c => c != first && (game[c.Row, c.Column] == game[first.Row, first.Column]) == sameValue);
            return (first, second);
        }

        private class InMemoryGameRepository : IGameRepository
        {
            private readonly Dictionary<int, GameDto> _games = new Dictionary<int, GameDto>();

            private int _lastId;

            public GameDto GetByGameId(int gameId)
            {
                return _games.TryGetValue(gameId, out var game) ? Copy(game) : null;
            }

            public IEnumerable<GameDto> GetByUserId(int userId)
            {
                return _games.Values.Where(g => g.UserId == userId).OrderBy(g => g.Id).Select(Copy).ToList();
            }

            public void Remove(int gameId)
            {
                _games.Remove(gameId);
            }

            public int Save(GameDto gameDto)
            {
                var game = Copy(gameDto);
                if (game.Id == 0)
                {
                    game.Id = ++_lastId;
                }

                _games[game.Id] = game;
                return game.Id;
            }

            private static GameDto Copy(GameDto source)
            {
                var result = new GameDto
                {
                    Id = source.Id,
                    UserId = source.UserId,
                    MoveCount = source.MoveCount,
                };

                Array.Copy(source.Cells, result.Cells, source.Cells.Length);
                Array.Copy(source.States, result.States, source.States.Length);
                return result;
            }
        }
    }
}
