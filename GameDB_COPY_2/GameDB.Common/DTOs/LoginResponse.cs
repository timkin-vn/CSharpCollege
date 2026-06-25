namespace GameDB.Common.DTOs;

public class LoginResponse
{
    public bool Success { get; set; }
    public int? PlayerId { get; set; }
    public string? Username { get; set; }
    public string? Message { get; set; }
}