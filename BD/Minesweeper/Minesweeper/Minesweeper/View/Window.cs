using Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model
{
    public class Window
    {
        private GameEngine _gameEngine = new GameEngine();

        public int CellNumber => _gameEngine.Cell;
    }
}
