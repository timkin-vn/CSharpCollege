using System.Collections.Generic;

namespace FifteenGame.Business.Models
{
    // Главный класс сохранения
    public class GameSaveData
    {
        public double ElapsedSeconds { get; set; }

        // Данные для ИИ
        public int AiLastX { get; set; }
        public int AiLastY { get; set; }
        public bool AiHunting { get; set; }

        // Списки кораблей (координаты каждого корабля)
        public List<List<PointDto>> PlayerShips { get; set; } = new List<List<PointDto>>();
        public List<List<PointDto>> EnemyShips { get; set; } = new List<List<PointDto>>();

        // Состояния клеток (попадания, промахи)
        public List<CellStateDto> PlayerFieldStates { get; set; } = new List<CellStateDto>();
        public List<CellStateDto> EnemyFieldStates { get; set; } = new List<CellStateDto>();
    }

    public class PointDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class CellStateDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int State { get; set; } // int соответствует enum CellState
    }
}