using System;

namespace FifteenGame.Common.BusinessModels
{
    public class MazeGameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SerializedGameManager { get; set; } // Stores the serialized GameManager state
    }
}
