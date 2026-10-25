using BlackjackMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackjackMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}