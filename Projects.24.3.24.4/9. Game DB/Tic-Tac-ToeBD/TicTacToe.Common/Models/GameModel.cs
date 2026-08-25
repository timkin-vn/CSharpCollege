using System;

namespace TicTacToe.Common.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public char[] Board { get; set; } = new char[9];
        public bool IsXTurn { get; set; } = true;

        public GameModel()
        {
            for (int i = 0; i < 9; i++) Board[i] = ' ';
        }
    }
}