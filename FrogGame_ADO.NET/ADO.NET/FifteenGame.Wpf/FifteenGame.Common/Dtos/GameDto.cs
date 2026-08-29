using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int FrogRow { get; set; }

        public int FrogColumn { get; set; }

        public int HomeRow { get; set; }

        public int HomeColumn { get; set; }

        public bool IsGameOver { get; set; }

        public bool IsWin { get; set; }

        public int? SelectedLilyPadRow { get; set; }

        public int? SelectedLilyPadColumn { get; set; }

        public int MinMoveCount { get; set; }
        public int BestMoveCount { get; set; }
        public int[,] Cells { get; set; } = new int[8, 8];
    }
}