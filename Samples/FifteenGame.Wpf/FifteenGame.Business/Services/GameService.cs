using System;
using System.Linq;
using WordGame.Models;

namespace WordGame.Services
{
    public class GameService
    {
        private readonly Random _random = new Random();
        private static readonly char[] AvailableLetters = { 'A', 'B', 'G', 'X', 'C', 'O' };

        public string GenerateRandomWord(int length)
        {
            return new string(Enumerable.Range(0, length).Select(_ => AvailableLetters[_random.Next(AvailableLetters.Length)]).ToArray());
        }

        public void InitializeGame(GameModel model, int difficulty)
        {
            string targetWord = GenerateRandomWord(6);
            model.Initialize(targetWord, difficulty);
        }

        public bool MakeMove(GameModel model, int row, int col)
        {

            return false;
        }

        public bool IsGameOver(GameModel model)
        {
            return model.IsWordCompleted();
        }
    }
}