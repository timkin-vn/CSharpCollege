using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2048Game.Web.Models
{
    public class GameViewModel
    {
        public TileViewModel[,] Tiles { get; set; }
        public bool CanMove { get; set; }
        public bool HasWon { get; set; }
        public int MoveCount { get; set; }
        public int RemainingBoxes { get; set; }
        public int RemainingTargets { get; set; }

        public GameViewModel(SokobanBoard board)
        {
            int rows = board.Rows;
            int cols = board.Cols;
            Tiles = new TileViewModel[rows, cols];

            int boxes = 0; int targets = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var tileType = board.GetTile(r, c);
                    Tiles[r, c] = new TileViewModel
                    {
                        Type = tileType
                    };

                    if (tileType == TileType.Box) boxes++;
                    if (tileType == TileType.Target) targets++;
                }
            }

            RemainingBoxes = boxes;
            RemainingTargets = targets;

            CanMove = board.CanMove();
            HasWon = board.IsSolved();
            MoveCount = board.MoveCount;
        }
    }
}