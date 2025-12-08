using Minesweeper.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Business.Services
{
    public class MinesweeperService
    {
        public MinesweeperField CreateNewGame(int size = 10, int mineCount = 15)
        {
            return new MinesweeperField(size, mineCount);
        }

        public void RevealCell(MinesweeperField field, int row, int col)
        {
            field.RevealCell(row, col);
        }

        public void ToggleFlag(MinesweeperField field, int row, int col)
        {
            field.ToggleFlag(row, col);
        }

        public string GetGameStatus(MinesweeperField field)
        {
            if (field.GameOver) return "GameOver";
            if (field.GameWon) return "GameWon";
            return "InProgress";
        }
    }
}