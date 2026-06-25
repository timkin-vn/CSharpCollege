using FifteenGame.Common.Definitions;
using System.Collections.Generic;

namespace FifteenGame.Common.BusinessDtos
{
    public class MazeGameReply
    {
        public LevelDto Level { get; set; }
        public PlayerDto Player { get; set; }
    }

    public class LevelDto
    {
        public CellDto[,] Grid { get; set; }
        public PlayerDto Player { get; set; }
        public (int, int) ExitPosition { get; set; }
        public Dictionary<(int, int), bool> DoorStates { get; set; } = new Dictionary<(int, int), bool>();
        public Dictionary<(int, int), bool> SwitchStates { get; set; } = new Dictionary<(int, int), bool>();
    }

    public class CellDto
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CellType Type { get; set; }
    }

    public class PlayerDto
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Keys { get; set; }
        public int Moves { get; set; }
    }

    public class MakeMazeMoveRequest
    {
        public int UserId { get; set; }
        public int DeltaRow { get; set; }
        public int DeltaCol { get; set; }
    }
}
