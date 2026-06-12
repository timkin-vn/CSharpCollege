using Game2048.Business.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            model.Reset();
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            return model.MakeMove(direction);
        }

        public bool IsGameOver(GameModel model)
        {
            return model.IsGameOver;
        }

        public void Shuffle(GameModel model)
        {
            model.Reset();
        }
    }
}
