using FifteenGame.Common.Definitions;
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

        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        public int MoveCount { get; set; }

        public int Difficulty { get; set; } = 1; 

        public int Theme { get; set; } = 1;  

        public int Score { get; set; }  

        public int ComboStreak { get; set; }   

        public int HintsLeft { get; set; } = Constants.DefaultHintsCount; 

        public int ElapsedSeconds { get; set; }  
    }
}
