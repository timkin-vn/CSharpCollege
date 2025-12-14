
using System;
using System.Collections.Generic;
using System.Linq;

namespace StepByStepPacman.Business.Models
{
    public class Ghost : GameObject
    {
        public string Color { get; set; }
        

        public Ghost(int x, int y, int size, string color) : base(x, y, size)
        {
            Color = color;
        }

        
        public void Move(int[,] gameBoard, PacmanPlayer player)
        {
            
            var possibleMoves = new List<(int dx, int dy)>
            {
                (0, 1), (0, -1), (1, 0), (-1, 0)
            };

            var validMoves = possibleMoves
                .Where(move =>
                    X + move.dx >= 0 && X + move.dx < gameBoard.GetLength(1) &&
                    Y + move.dy >= 0 && Y + move.dy < gameBoard.GetLength(0) &&
                    gameBoard[Y + move.dy, X + move.dx] != 0
                )
                .ToList();

            if (validMoves.Any())
            {
                var random = new Random();
                var move = validMoves[random.Next(validMoves.Count)];

                X += move.dx;
                Y += move.dy;
            }
        }
    }
}