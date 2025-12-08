using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Игра.DataAccess.EF.Entites
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}