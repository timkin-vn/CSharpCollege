using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace PacmanGame.DataAccess.Entities
{
    public class GameStateEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        // Основные поля игры
        public int Score { get; set; }
        public int Level { get; set; }

        
        public int Lives { get; set; }              
        public int PlayerX { get; set; }            
        public int PlayerY { get; set; }            

        [MaxLength(4000)]
        public string BoardState { get; set; }      

        [MaxLength(200)]
        public string GhostsPositions { get; set; } 

        // Поле для даты/времени
        public DateTime CreatedAt { get; set; }     

        // Связь с пользователем
        public int GameUserId { get; set; }
        [ForeignKey("GameUserId")]
        public virtual GameUserEntity GameUser { get; set; }

        
        public bool IsGameOver { get; set; }
    }
}