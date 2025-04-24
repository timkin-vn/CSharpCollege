using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.DataAccess.EF.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
