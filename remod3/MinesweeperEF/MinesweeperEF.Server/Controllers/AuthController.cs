using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinesweeperEF.Contracts.Auth;
using MinesweeperEF.Server.Data;

namespace MinesweeperEF.Server.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase {
    private readonly AppDbContext _db;
    private readonly JwtOptions _jwtOptions;

    public AuthController(AppDbContext db, JwtOptions jwtOptions) {
        _db = db;
        _jwtOptions = jwtOptions;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) {
        if (await _db.Users.AnyAsync(u => u.UserName == request.UserName))
            return BadRequest("User already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _db.Users.Add(new User {
            UserName = request.UserName,
            PasswordHash = hash
        });

        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request) {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized();

        var claims = new[] {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthResponse(jwt);
    }
}
