using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Game> Games { get; set; }

        public int WinStreak { get; set; }
    }
}
