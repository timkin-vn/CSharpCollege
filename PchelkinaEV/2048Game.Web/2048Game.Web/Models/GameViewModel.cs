using _2048Game.Business.Models;
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
        public int Score { get; set; }
        public int HighScore { get; set; }

        public GameViewModel(GameBoard board)
        {
            Tiles = new TileViewModel[4, 4];

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Tiles[r, c] = new TileViewModel
                    {
                        Value = board.Tiles[r, c]
                    };
                }
            }

            CanMove = board.CanMove();
            HasWon = board.HasWon();
            Score = board.Score;
        }
    }

}