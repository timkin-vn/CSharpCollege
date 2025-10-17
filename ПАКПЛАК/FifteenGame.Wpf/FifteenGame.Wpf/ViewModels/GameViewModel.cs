using Pacmen.Business.Models;
using Pacmen.Business.Services;

namespace Pacmen.WPF.Models
{
    public class GameViewModel
    {
        public GameState GameState { get; }
        public GameService GameService { get; }
        public Direction CurrentDirection { get; set; }

        public GameViewModel()
        {
            GameState = new GameState();
            GameService = new GameService(GameState);
        }
    }
}