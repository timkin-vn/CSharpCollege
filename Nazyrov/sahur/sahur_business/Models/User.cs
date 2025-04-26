using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gg_web_business.Models
{
    [Table("users")] 
    public class User
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Column("username")]
        [Required]
        public string Username { get; set; }

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; }
    }
}

