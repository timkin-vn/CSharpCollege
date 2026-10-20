using System;

namespace TicTacToeGame.Common
{
    public class GameState
    {
        public char[] Board { get; set; } = new char[9]; // 9 клеток
        public bool IsXTurn { get; set; } = true;
        public string Status { get; set; } = "In Progress";
    }
}