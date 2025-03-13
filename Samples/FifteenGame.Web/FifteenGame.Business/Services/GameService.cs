using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private static readonly Random _random = new Random();
        private static readonly string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string _targetWord;

        public void Initialize(GameModel model)
        {
            model.SelectedCells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = _letters[_random.Next(_letters.Length)];
                }
            }

            _targetWord = GenerateTargetWord();
        }

        public bool IsGameOver(GameModel model)
        {
            var selectedLetters = model.SelectedCells.Select(cell => model[cell.Item1, cell.Item2]).ToList();
            return string.Join("", selectedLetters) == _targetWord;
        }

        public void SelectLetter(GameModel model, int row, int column)
        {
            if (model.SelectedCells.Count == 0 ||
                model.SelectedCells.Last().Item1 == row ||
                model.SelectedCells.Last().Item2 == column)
            {
                model.SelectedCells.Add((row, column));
            }
        }

        public string GetTargetWord()
        {
            return _targetWord;
        }

        private string GenerateTargetWord()
        {
            var wordLength = _random.Next(3, 8);
            var word = new char[wordLength];
            for (int i = 0; i < wordLength; i++)
            {
                word[i] = _letters[_random.Next(_letters.Length)];
            }
            return new string(word);
        }
    }
}