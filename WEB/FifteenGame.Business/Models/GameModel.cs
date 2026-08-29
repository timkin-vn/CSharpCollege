
using System.Collections.Generic;


namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int Size = 10;

        public CellState[,] PlayerField { get; set; } = new CellState[Size, Size];
        public CellState[,] PlayerFog { get; set; } = new CellState[Size, Size];

        public CellState[,] ComputerField { get; set; } = new CellState[Size, Size];
        public CellState[,] ComputerFog { get; set; } = new CellState[Size, Size];

        public List<Ship> PlayerShips { get; set; } = new List<Ship>();
        public List<Ship> ComputerShips { get; set; } = new List<Ship>();

        public bool IsPlayerTurn { get; set; } 
        public bool IsSetupPhase { get; set; } = true;
        public bool GameOver { get; set; } = false;
        public bool PlayerWon { get; set; } = false;

        public Ship CurrentPlacingShip { get; set; }
        public bool IsHorizontal { get; set; } = true;

        
        public int ShipsToPlace { get; set; } = 10;
    }
}