using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Common.BusinessDtos
{
    public class UserRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
    }
}