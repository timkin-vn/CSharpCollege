using Blackjack.Business.Services;
using Blackjack.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private const int UserId = 1;

        [TestMethod]
        public void NewGameDealsTwoCardsEach()
        {
            var service = new GameService(new FakeGameRepository());

            var game = service.StartNewGame(UserId);

            Assert.AreEqual(2, game.PlayerCards.Count);
            Assert.AreEqual(2, game.DealerCards.Count);
            Assert.AreEqual(48, game.Deck.Count);
            Assert.AreNotEqual(0, game.Id);
        }

        [TestMethod]
        public void CardsAreNotRepeated()
        {
            var service = new GameService(new FakeGameRepository());

            var game = service.StartNewGame(UserId);
            var all = game.PlayerCards.Concat(game.DealerCards).Concat(game.Deck)
                .Select(c => (c.Rank, c.Suit))
                .ToList();

            Assert.AreEqual(52, all.Count);
            Assert.AreEqual(52, all.Distinct().Count());
        }

        [TestMethod]
        public void StandFinishesGameAndDealerHasAtLeast17()
        {
            var repository = new FakeGameRepository();
            var service = new GameService(repository);
            var game = service.StartNewGame(UserId);
            game.Status = GameStatus.Playing; // на случай блэкджека с раздачи
            repository.Save(game);

            game = service.Stand(game.Id);

            Assert.IsTrue(game.IsFinished);
            Assert.IsTrue(game.DealerScore >= 17);
        }

        [TestMethod]
        public void HitAfterFinishDoesNothing()
        {
            var service = new GameService(new FakeGameRepository());
            var game = service.Stand(service.StartNewGame(UserId).Id);
            int cards = game.PlayerCards.Count;

            game = service.Hit(game.Id);

            Assert.AreEqual(cards, game.PlayerCards.Count);
        }

        [TestMethod]
        public void BustIsLoss()
        {
            var repository = new FakeGameRepository();
            var service = new GameService(repository);
            var game = service.StartNewGame(UserId);

            // Подкладываем игроку 20 очков, а сверху колоды — десятку
            game.Status = GameStatus.Playing;
            game.PlayerCards = new List<Card>
            {
                new Card { Rank = Rank.King, Suit = Suit.Hearts },
                new Card { Rank = Rank.Queen, Suit = Suit.Hearts },
            };
            game.Deck.Insert(0, new Card { Rank = Rank.Ten, Suit = Suit.Clubs });
            repository.Save(game);

            game = service.Hit(game.Id);

            Assert.AreEqual(GameStatus.DealerWin, game.Status);
            Assert.AreEqual(1, service.GetStats(UserId).Losses);
        }

        [TestMethod]
        public void NewGameWhileUnfinishedCountsAsLoss()
        {
            var repository = new FakeGameRepository();
            var service = new GameService(repository);
            var first = service.StartNewGame(UserId);
            first.Status = GameStatus.Playing;
            repository.Save(first);

            service.StartNewGame(UserId);

            Assert.AreEqual(GameStatus.DealerWin, repository.GetById(first.Id).Status);
        }
    }
}
