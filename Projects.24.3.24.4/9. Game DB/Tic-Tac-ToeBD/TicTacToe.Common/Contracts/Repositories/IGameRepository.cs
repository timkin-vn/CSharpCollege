using TicTacToe.Common.Models;

public interface IGameRepository
{
    void UpdateGame(GameModel game);
    GameModel GetById(int id);
}