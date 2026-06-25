using System.Collections.Generic;

namespace FifteenGame.Business.Models
{
    public class Level
    {
        public const int Rows = 32;
        public const int Columns = 32;

        public Cell[,] Grid { get; private set; }
        public List<(int Row, int Col)> SwitchPositions { get; private set; }
        public Dictionary<(int Row, int Col), bool> SwitchStates { get; private set; }
        public Dictionary<(int Row, int Col), bool> DoorStates { get; private set; } // новое
        public (int Row, int Col) StartPosition { get; set; }
        public (int Row, int Col) DoorPosition { get; set; } // можно оставить для совместимости
        public (int Row, int Col) ExitPosition { get; set; }
        public bool IsDoorOpen { get; set; }

        public Level()
        {
            Grid = new Cell[Rows, Columns];
            SwitchPositions = new List<(int, int)>();
            SwitchStates = new Dictionary<(int, int), bool>();
            DoorStates = new Dictionary<(int, int), bool>();
            IsDoorOpen = false;
        }

        public void SetCell(int row, int col, CellType type)
        {
            Grid[row, col] = new Cell(type, row, col);
        }

        public Cell GetCell(int row, int col)
        {
            return Grid[row, col];
        }

        public void AddSwitch(int row, int col)
        {
            SwitchPositions.Add((row, col));
            SwitchStates[(row, col)] = false;
            SetCell(row, col, CellType.Switch);
        }

        public void ToggleSwitch(int row, int col)
        {
            if (SwitchStates.ContainsKey((row, col)))
                SwitchStates[(row, col)] = !SwitchStates[(row, col)];
        }

        public bool AreAllSwitchesActive()
        {
            foreach (var state in SwitchStates.Values)
                if (!state) return false;
            return true;
        }
    }
}