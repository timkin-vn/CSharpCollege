using Microsoft.AspNetCore.Mvc;
using Game2048.Business.Models;
using Game2048.Business.Services;

namespace Game2048.WebApi.Controllers;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;
    private static GameModel _serverGameModel = new GameModel();

    public GameController(GameService gameService)
    {
        _gameService = gameService;
        if (_serverGameModel.Score == 0 && _serverGameModel.Board[0, 0] == 0)
        {
            _serverGameModel.Reset();
        }
    }

    [HttpPost("restart")]
    public ActionResult<GameModel> Restart()
    {
        _serverGameModel.Reset();
        return Ok(_serverGameModel);
    }

    [HttpPost("move")]
    public ActionResult<GameModel> Move([FromQuery] MoveDirection direction)
    {
        _gameService.MakeMove(_serverGameModel, direction);
        return Ok(_serverGameModel);
    }
}
