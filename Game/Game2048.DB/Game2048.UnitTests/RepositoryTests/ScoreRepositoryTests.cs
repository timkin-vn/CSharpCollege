using Game2048.Common.Models;
using Game2048.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Game2048.UnitTests.RepositoryTests
{
    [TestClass]
    public class ScoreRepositoryTests
    {
        private ScoreRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _repository = new ScoreRepository();
        }

        [TestMethod]
        public void AddScore_ShouldAddNewScore()
        {
            // Arrange
            string playerName = "TestPlayer";
            int score = 1000;

            // Act
            _repository.AddScore(playerName, score);

            // Assert
            var scores = _repository.GetTopScores();
            Assert.IsTrue(scores.Any(s => s.Name == playerName && s.Score == score));
        }

        [TestMethod]
        public void GetTopScores_ShouldReturnScoresInDescendingOrder()
        {
            // Arrange
            _repository.AddScore("Player1", 100);
            _repository.AddScore("Player2", 500);
            _repository.AddScore("Player3", 300);

            // Act
            var scores = _repository.GetTopScores().ToList();

            // Assert
            Assert.IsTrue(scores.Count >= 3);
            Assert.AreEqual(500, scores[0].Score);
            Assert.AreEqual(300, scores[1].Score);
            Assert.AreEqual(100, scores[2].Score);
        }

        [TestMethod]
        public void GetTopScores_ShouldLimitToTop10()
        {
            // Arrange
            for (int i = 1; i <= 15; i++)
            {
                _repository.AddScore($"Player{i}", i * 100);
            }

            // Act
            var scores = _repository.GetTopScores().ToList();

            // Assert
            Assert.AreEqual(10, scores.Count);
            Assert.AreEqual(1500, scores[0].Score); // Highest score
            Assert.AreEqual(600, scores[9].Score);   // 10th highest score
        }

        [TestMethod]
        public void AddScore_ShouldHandleEmptyPlayerName()
        {
            // Arrange
            string emptyName = "";
            int score = 200;

            // Act & Assert - Should not throw exception
            _repository.AddScore(emptyName, score);
            var scores = _repository.GetTopScores();
            Assert.IsTrue(scores.Any(s => s.Score == score));
        }

        [TestMethod]
        public void GetTopScores_ShouldReturnEmptyListWhenNoScores()
        {
            // Act
            var scores = _repository.GetTopScores();

            // Assert
            Assert.IsNotNull(scores);
            Assert.AreEqual(0, scores.Count());
        }
    }
}
