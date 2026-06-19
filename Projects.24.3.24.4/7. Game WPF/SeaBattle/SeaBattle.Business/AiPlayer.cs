using System;
using System.Collections.Generic;

namespace SeaBattle.Business
{
    
    public class AiPlayer
    {
        private readonly Random _rnd = new Random();
        private readonly Queue<int[]> _queue = new Queue<int[]>();
        private readonly bool[,] _tried = new bool[Board.Size, Board.Size];

        
        public ShotResult Fire(Board playerBoard, out int row, out int col)
        {
            row = -1;
            col = -1;

            
            while (_queue.Count > 0)
            {
                int[] cell = _queue.Dequeue();
                if (!_tried[cell[0], cell[1]])
                {
                    row = cell[0];
                    col = cell[1];
                    break;
                }
            }

            
            if (row == -1)
            {
                var free = new List<int[]>();
                for (int r = 0; r < Board.Size; r++)
                    for (int c = 0; c < Board.Size; c++)
                        if (!_tried[r, c])
                            free.Add(new[] { r, c });

                if (free.Count == 0)
                    return ShotResult.Invalid;

                int[] pick = free[_rnd.Next(free.Count)];
                row = pick[0];
                col = pick[1];
            }

            _tried[row, col] = true;
            ShotResult result = playerBoard.Shoot(row, col);

            if (result == ShotResult.Hit)
            {
                int[][] deltas = { new[] { -1, 0 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { 0, 1 } };
                foreach (var d in deltas)
                {
                    int nr = row + d[0];
                    int nc = col + d[1];
                    if (nr >= 0 && nr < Board.Size && nc >= 0 && nc < Board.Size && !_tried[nr, nc])
                        _queue.Enqueue(new[] { nr, nc });
                }
            }
            else if (result == ShotResult.Sunk)
            {
                _queue.Clear(); 
            }

            return result;
        }
    }
}
