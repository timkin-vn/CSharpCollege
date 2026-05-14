using System.Data.Entity;

namespace Server.Models
{
    public class MinesweeperContext : DbContext
    {
        public MinesweeperContext() : base("name=MinesweeperDb")
        {
        }

    }
}