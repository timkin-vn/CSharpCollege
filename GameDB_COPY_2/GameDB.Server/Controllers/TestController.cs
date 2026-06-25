using Microsoft.AspNetCore.Mvc;

namespace GameDB.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new 
        { 
            Status = "OK", 
            Message = "GameDB Server is running!",
            Timestamp = DateTime.Now
        });
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(new { Pong = true, Time = DateTime.Now });
    }
}