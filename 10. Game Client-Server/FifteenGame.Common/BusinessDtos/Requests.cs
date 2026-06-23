namespace FifteenGame.Common.BusinessDtos
{
    public class LoginRequest { public string Username { get; set; } public string Password { get; set; } }
    public class UserDto { public int Id { get; set; } public string Username { get; set; } public int BestScore { get; set; } }
    public class CreateGameRequest { public int UserId { get; set; } public int Mode { get; set; } }
    public class MoveRequest { public int GameId { get; set; } public int Row { get; set; } public int Column { get; set; } }
}