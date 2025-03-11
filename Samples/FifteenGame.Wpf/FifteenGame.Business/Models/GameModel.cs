using System;

namespace WordGame.Models
{
    public class GameModel
    {
        public const int RowCount = 7;
        public const int ColumnCount = 7;
        private char[,] _cells = new char[RowCount, ColumnCount];
        private static readonly char[] AvailableLetters = { 'A', 'B', 'G', 'X', 'C', 'O' };

        public char this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public string TargetWord { get; set; }
        public int Score { get; set; }

        public void Initialize(string targetWord, int difficulty)
        {
            TargetWord = targetWord;
            Score = 0;
            FillBoard(difficulty);
        }

        private void FillBoard(int difficulty)
        {
            var rnd = new Random();
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    _cells[row, col] = AvailableLetters[rnd.Next(AvailableLetters.Length)];
                }
            }


            EnsureWordCanBeFormed(difficulty);
        }

        private void EnsureWordCanBeFormed(int difficulty)
        {

        }

        public bool IsWordCompleted()
        {

            return false;
        }
    }
}