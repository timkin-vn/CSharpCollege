using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.DataContext
{
    public class FifteenGameDataContext : DbContext
    {
        static FifteenGameDataContext()
        {
            System.Data.Entity.Database
                .SetInitializer<FifteenGameDataContext>(null);
        }
        public FifteenGameDataContext() : base("EFMain")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Cell> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
        public void Delete(int id)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.Find(id);

                if (user == null)
                    return;

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
