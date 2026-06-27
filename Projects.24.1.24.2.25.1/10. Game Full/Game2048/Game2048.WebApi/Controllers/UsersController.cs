using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game2048.WebApi.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("api/users/get-all")]
    public UsersReply GetAllUsers()
        => new() { Users = _userService.GetAllUsers().Select(ToDto).ToList() };

    [HttpPost("api/users/get-or-create")]
    public UserReply GetOrCreateUser([FromBody] UserNameRequest request)
        => ToDto(_userService.GetOrCreateUser(request.UserName));

    [HttpGet("api/users/get-by-name/{userName}")]
    public ActionResult<UserReply> GetUserByName(string userName)
    {
        var user = _userService.GetUserByName(userName);
        return user == null ? NotFound() : ToDto(user);
    }

    private static UserReply ToDto(UserModel user)
        => new() { Id = user.Id, Name = user.Name };
}
