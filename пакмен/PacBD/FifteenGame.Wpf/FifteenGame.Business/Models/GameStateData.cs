// StepByStepPacman.Business.Models/GameStateData.cs
using System;

namespace StepByStepPacman.Business.Models
{
    public class GameStateData
    {
        public int Id { get; set; } // ID сохранения в БД
        public int UserId { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public int Level { get; set; } 
        public int CollectedDots { get; set; }
        public int TotalDots { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public string BoardState { get; set; } // Сериализованное поле
        public string GhostsPositions { get; set; } // Сериализованные призраки
        public DateTime CreatedAt { get; set; }
    }
}