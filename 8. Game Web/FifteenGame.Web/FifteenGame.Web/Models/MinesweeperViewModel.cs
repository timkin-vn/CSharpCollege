using Minesweeper.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Web.Models
{
    public class MinesweeperViewModel
    {
        public MinesweeperField GameField { get; set; }
        public bool DebugMode { get; set; } = false;
        public int Size => GameField?.Size ?? 10;

        public MinesweeperViewModel() { }

        public MinesweeperViewModel(MinesweeperField gameField)
        {
            GameField = gameField;
        }
    }
}