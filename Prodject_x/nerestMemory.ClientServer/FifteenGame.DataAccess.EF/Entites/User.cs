using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entites
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
