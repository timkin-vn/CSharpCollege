using GameDB.Common.Enums;

namespace GameDB.Server.Models;

public class MoveRequest
{
    public int PlayerId { get; set; }
    public MoveDirection Direction { get; set; }
}