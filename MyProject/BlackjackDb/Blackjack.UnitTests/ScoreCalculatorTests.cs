using Blackjack.Common.Models;
using Blackjack.Common.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.UnitTests
{
    [TestClass]
    public class ScoreCalculatorTests
    {
        private static Card C(Rank rank) => new Card { Rank = rank, Suit = Suit.Spades };

        [TestMethod]
        public void PictureCardsAreTen()
        {
            Assert.AreEqual(30, ScoreCalculator.Calculate(new[] { C(Rank.Jack), C(Rank.Queen), C(Rank.King) }));
        }

        [TestMethod]
        public void AceIsElevenWhenNoBust()
        {
            Assert.AreEqual(21, ScoreCalculator.Calculate(new[] { C(Rank.Ace), C(Rank.King) }));
        }

        [TestMethod]
        public void TwoAcesAreTwelve()
        {
            Assert.AreEqual(12, ScoreCalculator.Calculate(new[] { C(Rank.Ace), C(Rank.Ace) }));
        }

        [TestMethod]
        public void AceBecomesOneOnBust()
        {
            Assert.AreEqual(16, ScoreCalculator.Calculate(new[] { C(Rank.Ace), C(Rank.Five), C(Rank.Queen) }));
        }
    }
}
