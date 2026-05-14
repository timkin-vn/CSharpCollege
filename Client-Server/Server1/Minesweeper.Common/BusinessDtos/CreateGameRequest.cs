using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Common.BusinessDtos
{
    public class CreateGameRequest
    {
        [Required]
        public int UserId { get; set; }

        [Range(5, 20)]
        public int Size { get; set; } = 10;

        [Range(1, 100)]
        public int MineCount { get; set; } = 15;
    }
}